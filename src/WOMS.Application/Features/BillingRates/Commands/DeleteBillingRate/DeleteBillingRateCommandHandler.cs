using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingRates.Commands.DeleteBillingRate
{
    public class DeleteBillingRateCommandHandler : IRequestHandler<DeleteBillingRateCommand>
    {
        private readonly IRateTableRepository _rateTableRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteBillingRateCommandHandler(
            IRateTableRepository rateTableRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _rateTableRepository = rateTableRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Handle(DeleteBillingRateCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            var rateTable = await _rateTableRepository.GetByIdAsync(request.Id, cancellationToken);
            if (rateTable == null)
            {
                throw new KeyNotFoundException($"Rate table with ID '{request.Id}' not found.");
            }

            // Soft delete
            rateTable.IsDeleted = true;
            rateTable.DeletedBy = userId;
            rateTable.DeletedOn = DateTime.UtcNow;

            await _rateTableRepository.UpdateAsync(rateTable, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

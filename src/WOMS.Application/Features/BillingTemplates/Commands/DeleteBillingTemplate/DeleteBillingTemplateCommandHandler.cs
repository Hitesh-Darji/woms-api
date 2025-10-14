using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingTemplates.Commands.DeleteBillingTemplate
{
    public class DeleteBillingTemplateCommandHandler : IRequestHandler<DeleteBillingTemplateCommand, bool>
    {
        private readonly IBillingTemplateRepository _billingTemplateRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteBillingTemplateCommandHandler(
            IBillingTemplateRepository billingTemplateRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _billingTemplateRepository = billingTemplateRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteBillingTemplateCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            var billingTemplate = await _billingTemplateRepository.GetByIdAsync(request.Id, cancellationToken);
            if (billingTemplate == null)
            {
                throw new InvalidOperationException($"Billing template with ID '{request.Id}' not found.");
            }

            // Soft delete
            billingTemplate.IsDeleted = true;
            billingTemplate.DeletedBy = userId;
            billingTemplate.DeletedOn = DateTime.UtcNow;

            await _billingTemplateRepository.UpdateAsync(billingTemplate, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

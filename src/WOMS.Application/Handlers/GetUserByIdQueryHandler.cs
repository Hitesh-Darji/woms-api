using MediatR;
using WOMS.Application.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Application.Queries;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }
    }
}

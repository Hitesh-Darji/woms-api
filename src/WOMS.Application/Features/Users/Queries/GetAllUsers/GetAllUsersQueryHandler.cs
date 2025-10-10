using AutoMapper;
using MediatR;
using WOMS.Application.Features.Users.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllActiveAsync(cancellationToken);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}

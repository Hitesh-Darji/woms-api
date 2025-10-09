using AutoMapper;
using MediatR;
using WOMS.Application.Features.Users.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IUserRepository _userRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, AutoMapper.IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdActiveAsync(request.Id, cancellationToken);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Features.Roles.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AutoMapper.IMapper _mapper;

        public GetAllRolesQueryHandler(RoleManager<ApplicationRole> roleManager, AutoMapper.IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles.OrderBy(r => r.Name).ToList();
            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Task.FromResult(roleDtos);
        }
    }
}

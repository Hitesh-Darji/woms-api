using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Features.Roles.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AutoMapper.IMapper _mapper;

        public GetRoleByIdQueryHandler(RoleManager<ApplicationRole> roleManager, AutoMapper.IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);
            return role != null ? _mapper.Map<RoleDto>(role) : null;
        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Features.Roles.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDto>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AutoMapper.IMapper _mapper;

        public UpdateRoleCommandHandler(RoleManager<ApplicationRole> roleManager, AutoMapper.IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            // Get the existing role
            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                throw new InvalidOperationException($"Role with ID '{request.Id}' not found.");
            }

            // Check if name is being changed and if new name already exists
            if (role.Name != request.Name)
            {
                if (await _roleManager.RoleExistsAsync(request.Name))
                {
                    throw new InvalidOperationException($"Role with name '{request.Name}' already exists.");
                }
            }

            // Update role properties
            role.Name = request.Name;
            role.Description = request.Description;
            role.UpdatedBy = request.UpdatedBy;
            role.UpdatedOn = DateTime.UtcNow;

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to update role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            var roleDto = _mapper.Map<RoleDto>(role);

            return roleDto;
        }
    }
}

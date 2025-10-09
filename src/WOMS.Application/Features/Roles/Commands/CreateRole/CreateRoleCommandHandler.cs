using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Features.Roles.DTOs;
using WOMS.Domain.Entities;

namespace WOMS.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AutoMapper.IMapper _mapper;

        public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager, AutoMapper.IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            // Check if role with name already exists
            if (await _roleManager.RoleExistsAsync(request.Name))
            {
                throw new InvalidOperationException($"Role with name '{request.Name}' already exists.");
            }

            var role = new ApplicationRole
            {
                Name = request.Name,
                Id = Guid.NewGuid(),
                Description = request.Description,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsClient = false,
                IsDeleted = false
            };

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Failed to create role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            var roleDto = _mapper.Map<RoleDto>(role);

            return roleDto;
        }
    }
}

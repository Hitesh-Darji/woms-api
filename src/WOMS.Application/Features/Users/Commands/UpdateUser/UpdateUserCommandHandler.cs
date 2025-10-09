using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Features.Users.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            AutoMapper.IMapper mapper,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Get the existing user
            var user = await _userRepository.GetByIdActiveAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID '{request.Id}' not found.");
            }

            // Check if email is being changed and if new email already exists
            if (user.Email != request.Email)
            {
                if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
                {
                    throw new InvalidOperationException($"User with email '{request.Email}' already exists.");
                }
            }

            // Extract FirstName and LastName from FullName
            var nameParts = request.FullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
            var lastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : string.Empty;

            // Update user properties
            user.FirstName = firstName;
            user.LastName = lastName;
            user.FullName = request.FullName;
            user.Address = request.Address;
            user.City = request.City;
            user.PostalCode = request.PostalCode;
            user.Phone = request.Phone;
            user.Email = request.Email;
            user.UserName = request.Email; // Update UserName to match Email
            user.UpdatedBy = request.UpdatedBy;
            user.UpdatedOn = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Update user role if it has changed
            var currentRoles = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            
            if (role != null)
            {
                // Remove user from all current roles
                if (currentRoles.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                }
                
                // Add user to new role
                await _userManager.AddToRoleAsync(user, role.Name!);
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.RoleId = request.RoleId;
            return userDto;
        }
    }
}

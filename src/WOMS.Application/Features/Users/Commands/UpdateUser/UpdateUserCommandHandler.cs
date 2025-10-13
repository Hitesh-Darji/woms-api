using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            AutoMapper.IMapper mapper,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }

            // Get the existing user using ID from token
            var user = await _userRepository.GetByIdActiveAsync(userIdClaim, cancellationToken);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID '{userIdClaim}' not found.");
            }

            // Check if email is being changed and if new email already exists
            if (user.Email != request.UpdateUserDto.Email)
            {
                if (await _userRepository.ExistsByEmailAsync(request.UpdateUserDto.Email, cancellationToken))
                {
                    throw new InvalidOperationException($"User with email '{request.UpdateUserDto.Email}' already exists.");
                }
            }

            // Extract FirstName and LastName from FullName
            var nameParts = request.UpdateUserDto.FullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
            var lastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : string.Empty;

            // Update user properties
            user.FirstName = firstName;
            user.LastName = lastName;
            user.FullName = request.UpdateUserDto.FullName;
            user.Address = request.UpdateUserDto.Address;
            user.City = request.UpdateUserDto.City;
            user.PostalCode = request.UpdateUserDto.PostalCode;
            user.Phone = request.UpdateUserDto.Phone;
            user.Email = request.UpdateUserDto.Email;
            user.UserName = request.UpdateUserDto.Email; // Update UserName to match Email
            user.UpdatedBy = userIdClaim;
            user.UpdatedOn = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Update user role if it has changed
            var currentRoles = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.FindByIdAsync(request.UpdateUserDto.RoleId.ToString());
            
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
            userDto.RoleId = request.UpdateUserDto.RoleId;
            return userDto;
        }
    }
}

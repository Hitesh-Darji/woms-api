using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WOMS.Application.Features.Users.Commands.CreateUser;
using WOMS.Application.Features.Users.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            AutoMapper.IMapper mapper,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Check if user with email already exists
            if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            {
                throw new InvalidOperationException($"User with email '{request.Email}' already exists.");
            }

            // Check if role exists in database
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
            {
                throw new ArgumentException($"Role with ID '{request.RoleId}' does not exist.", nameof(request.RoleId));
            }

            // Extract FirstName and LastName from FullName
            var nameParts = request.FullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
            var lastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : string.Empty;

            var user = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                FullName = request.FullName,
                Address = request.Address,
                City = request.City,
                PostalCode = request.PostalCode,
                Phone = request.Phone,
                Email = request.Email,
                UserName = request.Email, // Set UserName to Email for Identity
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Add user to role using UserManager and RoleManager
            await _userManager.AddToRoleAsync(user, role.Name!);

            var userDto = _mapper.Map<UserDto>(user);
            userDto.RoleId = request.RoleId;
            return userDto;
        }
    }
}

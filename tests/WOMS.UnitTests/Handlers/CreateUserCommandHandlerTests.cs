using FluentAssertions;
using Moq;
using WOMS.Application.Commands;
using WOMS.Application.DTOs;
using WOMS.Application.Handlers;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.UnitTests.Handlers
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _mapperMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateUser_WhenValidCommand()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
               // CreatedAt = DateTime.UtcNow
            };

            var userDto = new UserDto
            {
                Id = user.Id,
              //  FullName = user.FullName,
                Email = user.Email,
              //  CreatedAt = user.CreatedAt
            };

            _userRepositoryMock.Setup(x => x.ExistsByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            _mapperMock.Setup(x => x.Map<UserDto>(user))
                .Returns(userDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.FullName.Should().Be("John Doe");
            result.Email.Should().Be("john.doe@example.com");
            
            _userRepositoryMock.Verify(x => x.ExistsByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserWithEmailExists()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            _userRepositoryMock.Setup(x => x.ExistsByEmailAsync(command.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
            
            _userRepositoryMock.Verify(x => x.ExistsByEmailAsync(command.Email, It.IsAny<CancellationToken>()), Times.Once);
            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}

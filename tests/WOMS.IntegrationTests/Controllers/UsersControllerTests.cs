using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;
using WOMS.Application.DTOs;

namespace WOMS.IntegrationTests.Controllers
{
    public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public UsersControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task CreateUser_ShouldReturnCreatedUser_WhenValidData()
        {
            // Arrange
            var createUserDto = new CreateUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            var json = JsonSerializer.Serialize(createUserDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/users", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            var userDto = JsonSerializer.Deserialize<UserDto>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            userDto.Should().NotBeNull();
            userDto!.FullName.Should().Be("John Doe");
            userDto.Email.Should().Be("john.doe@example.com");
        }

        [Fact]
        public async Task GetUser_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var createUserDto = new CreateUserDto
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com"
            };

            var json = JsonSerializer.Serialize(createUserDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Create user first
            var createResponse = await _client.PostAsync("/api/users", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createResponseContent = await createResponse.Content.ReadAsStringAsync();
            var createdUser = JsonSerializer.Deserialize<UserDto>(createResponseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Act
            var getResponse = await _client.GetAsync($"/api/users/{createdUser!.Id}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var getResponseContent = await getResponse.Content.ReadAsStringAsync();
            var userDto = JsonSerializer.Deserialize<UserDto>(getResponseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            userDto.Should().NotBeNull();
            userDto!.FullName.Should().Be("Jane Smith");
            userDto.Email.Should().Be("jane.smith@example.com");
        }

        [Fact]
        public async Task GetUser_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/users/{nonExistentId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}

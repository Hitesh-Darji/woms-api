using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WOMS.Application.Features.Auth.Dtos;

namespace WOMS.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleResponse<T>(int statusCode, string successMessage, bool isSuccess, T data, List<string> errors = null)
        {
            var response = new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = successMessage,
                IsSuccess = isSuccess,
                Errors = errors,
                Data = data
            };
/*
            _logger.Info($"StatusCode: {response.StatusCode}, " +
                         $"Message: {response.Message}, " +
                         $"IsSuccess: {response.IsSuccess}, " +
                         $"Data: {JsonConvert.SerializeObject(data)}");*/

            return StatusCode(statusCode, response);
        }
    

        protected IActionResult ValidateRequest<T>(IValidator<T> validator, T request)
        {
            if (request == null || validator == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
              //  _logger.Error($"Validation failed for {typeof(T).Name}: {string.Join(", ", errors)}");

                return BadRequest(new ApiResponse<List<string>>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Validation failed",
                    Errors = errors,
                    IsSuccess = false,
                    Data = null
                });
            }
            return null;
        }

        protected Guid? UserId
        {
            get
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Guid.TryParse(userIdClaim, out var id) ? id : null;
            }
        }
    }
}

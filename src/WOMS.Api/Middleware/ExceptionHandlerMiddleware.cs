
using FluentValidation;
using Newtonsoft.Json;
using System.Net;
using WOMS.Application.Features.Auth.Dtos;

namespace WOMS.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.HasStarted) return;
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    context.Response.ContentType = "application/json";
                    var unauthorizedResponse = new ApiResponse<object>
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "You are not authorized to access this resource."
                    };

                    var result = JsonConvert.SerializeObject(unauthorizedResponse);
                    await context.Response.WriteAsync(result);
                    return;
                }
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    context.Response.ContentType = "application/json";
                    var unauthorizedResponse = new ApiResponse<object>
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden,
                        Message = "You don't have permission to access this resource."
                    };

                    var result = JsonConvert.SerializeObject(unauthorizedResponse);
                    await context.Response.WriteAsync(result);
                    return;
                }
            }
            catch (ValidationException ex)
            {
              
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var response = new ApiResponse<object>
                {
                    IsSuccess = false,
                    Errors = ex.Errors.Select(e => e.ErrorMessage).ToList()
                };

                var json = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {               
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private static async Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ApiResponse<object> exModel = new ApiResponse<object>();
            switch (exception)
            {
                case ApplicationException ex:
                    exModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.Message = ex.Message;
                    break;

                case FileNotFoundException ex:
                    exModel.StatusCode = (int)HttpStatusCode.NotFound;
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    exModel.Message = ex.Message;
                    break;

                case ArgumentNullException ex:
                    exModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.Message = ex.Message;
                    break;

                case ArgumentException ex:
                    exModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.Message = ex.Message;
                    break;

                case InvalidOperationException ex:
                    exModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.Message = ex.Message;
                    break;

                case UnauthorizedAccessException ex:
                    exModel.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    exModel.Message = ex.Message;
                    break;

                case NotSupportedException ex:
                    exModel.StatusCode = (int)HttpStatusCode.NotImplemented;
                    response.StatusCode = (int)HttpStatusCode.NotImplemented;
                    exModel.Message = ex.Message;
                    break;

                case KeyNotFoundException ex:
                    exModel.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exModel.Message = ex.Message;
                    break;
              
                default:
                    exModel.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exModel.Message = exception.Message;
                    exModel.Errors = new List<string> { exception.Message };
                    break;
            }

            var exResult = JsonConvert.SerializeObject(exModel);
            await context.Response.WriteAsync(exResult).ConfigureAwait(false);
        }
    }
}

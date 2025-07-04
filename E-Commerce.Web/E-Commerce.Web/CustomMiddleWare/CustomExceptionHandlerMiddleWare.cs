using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.Web.CustomMiddleWare
{
    public class customExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<customExceptionHandlerMiddleWare> _logger;
        public customExceptionHandlerMiddleWare(RequestDelegate next, ILogger<customExceptionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext httpContext )
        {

            try
            {
                await _next.Invoke(httpContext);

                await HandleNotFoundEndPointAsync(httpContext);

            }

            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while processing the request.");

                
                await HandleExceptionAsync(httpContext, ex);

            }

        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {

                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is not Found" ,

                };


                await httpContext.Response.WriteAsJsonAsync(Response);

            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var Response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            httpContext.Response.StatusCode = ex switch
            {

                NotFoundException => StatusCodes.Status404NotFound ,
                UnauthorizedException => StatusCodes.Status401Unauthorized , 
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException , Response )  ,
                _ => StatusCodes.Status500InternalServerError
            };
            //Set Content Type For Response
            httpContext.Response.ContentType = "application/json";
            //Respone Object
            //var errorResponse = new ErrorToReturn()
            //{
            //    StatusCode = httpContext.Response.StatusCode,
            //    ErrorMessage = ex.Message
            //};
            //Return object as JSON
            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}

using Domain.Exceptions;
using Shared;
using Shared.ErrorModels;

namespace Store.G02.Api.Middlewares
{
    //custome middleware for global exception handling
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;


        //ctor contains the address of the next delegate (from CLR)
        public GlobalErrorHandlingMiddleware(RequestDelegate next,ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        }

        //current middleware Function
        //httpcontext deals with request and response
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMassage = $"End Point{context.Request.Path} is not valid"
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                // Exeption log
                _logger.LogError(ex, ex.Message);
                //set status code for response
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //set content type for response
                context.Response.ContentType = "application/json";
                //set response object- response body
                //(we make object from the class which represents the error)
                var response = new ErrorDetails()
                {
                    ErrorMassage = ex.Message
                };

                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = response.StatusCode;

                //return response
                await context.Response.WriteAsJsonAsync(response);
                

            }

        }
    }
}

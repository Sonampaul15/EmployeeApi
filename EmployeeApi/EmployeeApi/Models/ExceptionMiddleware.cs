using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EmployeeApi.Models
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        public ExceptionMiddleware(RequestDelegate _requestDelegate)
        {
                requestDelegate= _requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch 
            {

               context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var error = new ErrorDetails
                {
                    Title = "Error into the appplication code",
                    Status = "Internal Server Error"
                };
                var result=JsonConvert.SerializeObject(error);
                await context.Response.WriteAsync(result);

            }
        }
    }
}

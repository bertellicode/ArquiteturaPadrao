using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace ArquiteturaPadrao.Services.Api.Handlers
{
    public class ExceptionHandler
    {
        public async Task Invoke(HttpContext context)
        {
            HttpStatusCode httpStatus = HttpStatusCode.InternalServerError;

            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (exception != null)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)httpStatus;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    ErroMessage = exception.StackTrace,
                    Status = httpStatus,
                }));
            }
        }
    }
}

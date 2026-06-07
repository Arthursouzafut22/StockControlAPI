using System.Net;
using System.Text.Json;

namespace ControleMercadoria.API.Configuration
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleException(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                await HandleException(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch(UnauthorizedAccessException ex)
            {
                await HandleException(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleException(context, HttpStatusCode.InternalServerError, "Erro interno no servidor.");
            }
        }
        private async Task HandleException(HttpContext context, HttpStatusCode status, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var response = new
            {
                status = (int)status,
                message = message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}




using Domain.Exceptions;

namespace PharmaTrack.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
			catch (NotFoundException ex)
			{
				HandleException(context, 404, ex.Message);
			}
			catch(BadRequestException ex)
			{
				HandleException(context, 400, ex.Message);
			}
			
            catch (Exception ex)
			{

				HandleException(context, 500, $"An unexpected error occurred: {ex.Message}");
            }

        }

		private void HandleException(HttpContext context,int statusCode, string message)
		{
			context.Response.StatusCode = statusCode;
			context.Response.ContentType = "application/json";
			var response = new
			{
				StatusCode = statusCode,
				Message = message,
				Success = false
			};
			context.Response.WriteAsJsonAsync(response);
		}
		

    }
}

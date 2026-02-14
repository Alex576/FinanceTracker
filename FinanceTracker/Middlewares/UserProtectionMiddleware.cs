public class UserProtectionMiddleware
{
    private readonly RequestDelegate _next;

    public UserProtectionMiddleware(RequestDelegate next) 
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      
        await _next(context);
    }
}
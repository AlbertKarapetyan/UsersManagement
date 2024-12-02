using Domain.Helpers;

namespace UsersManagement.Middlewares
{
    public class IdentityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public IdentityMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_configuration["JWTSecurityKey"] == null)
                throw new ArgumentNullException("JWTSecurityKey not found!");

            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader["Bearer ".Length..];
                context.Items["JWT"] = token;
                if (!JwtSecurity.ValidateJwt(token, _configuration["JWTSecurityKey"]!))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            await _next(context);
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


public class TokenRevocationMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentBag<string> RevokedTokens = new();

    public TokenRevocationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public static void RevokeToken(string token)
    {
        RevokedTokens.Add(token);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token) && RevokedTokens.Contains(token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token revocado.");
            return;
        }

        await _next(context);
    }
}

using ByteStore.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ByteStore.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class TokenValidationAttribute : TypeFilterAttribute
{
    public TokenValidationAttribute() : base(typeof(TokenValidationFilter))
    {
    }
}

public class TokenValidationFilter : IAuthorizationFilter
{
    private readonly ITokenService _tokenService;

    public TokenValidationFilter(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string token = context.HttpContext.Request.Headers["Authorization"];

        if (string.IsNullOrEmpty(token) || !IsBearerToken(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        token = token.Replace("Bearer ", "");

        if (!_tokenService.ValidateToken(token))
        {
            context.Result = new UnauthorizedResult();
        }
    }

    private static bool IsBearerToken(string token)
    {
        return token.StartsWith("Bearer ");
    }
}
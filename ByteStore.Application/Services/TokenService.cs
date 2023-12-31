﻿using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ByteStore.Application.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(int userId, string username, Roles role)
    {
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"));

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(5),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token, byte[] key)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, //true se quiser validar o emissor (issuer).
            ValidateAudience = false, // true se quiser validar a audiência (audience).
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // ajusta o tempo de tolerância.
        };

        try
        {
            tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return true;
        }
        catch (Exception)
        {
            // Invalid token.
            return false;
        }
    }
}
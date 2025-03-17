using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using NotesSeverstal.Context;
using NotesSeverstal.Core;
using NotesSeverstal.IService;
using NotesSeverstal.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NotesSeverstal.Middleware
{
    public class JwtSecurity
    {
        private readonly RequestDelegate _next;
        private readonly JwtSecurityTokenHandler _tokenHandler = new();

        public JwtSecurity(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Метод для обработки HTTP-запроса, проверяет JWT-токен, аутентифицирует пользователя.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["JwtToken"];

            var userService = context.RequestServices.GetRequiredService<IUserService>();
            var noteService = context.RequestServices.GetRequiredService<INoteService>();

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    context.User = ValidateToken(token);
                }
                catch (SecurityTokenExpiredException)
                {
                    context.Response.Cookies.Delete("JwtToken");
                    Console.WriteLine("Срок действия токена истек");
                }
            }
            else
            {
                var user = await GenerateToken(context);
                await userService.Create(user);
                await noteService.Add(new Note { Description = "Твоя первая заметка!", UserId = user.Id });
            }

            await _next(context);
        }

        /// <summary>
        ///     Проверяет и валидирует JWT-токен
        /// </summary>
        private ClaimsPrincipal ValidateToken(string token)
        {
            return new ClaimsPrincipal(_tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = AuthOption.Issuer,
                ValidAudience = AuthOption.Audience,
                IssuerSigningKey = AuthOption.GetSymmetricSecurityKey()
            }, out _));
        }

        /// <summary>
        ///     Генерирует JWT-токен и создает нового пользователя (гостя)
        /// </summary>
        private async Task<User> GenerateToken(HttpContext context)
        {
            var user = new User();

            var claims = new List<Claim> { new Claim("Id", user.Id) };

            var jwt = new JwtSecurityToken(
                issuer: AuthOption.Issuer,
                audience: AuthOption.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(AuthOption.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var token = _tokenHandler.WriteToken(jwt);
            context.Response.Cookies.Append("JwtToken", token);

            return user;
        }
    }
}

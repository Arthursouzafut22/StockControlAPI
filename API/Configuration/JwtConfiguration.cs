using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace ControleMercadoria.API.Configuration
{
    public static class JwtConfiguration
    {
        public static void AddJwtConfiguration(this WebApplicationBuilder builder)
        {
            var secretKey = builder.Configuration["Jwt:SecretKey"];

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(secretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var mensagem = JsonSerializer.Serialize(new
                            {
                                status = 401,
                                message = "Acesso negado! Token não informado."
                            });
                            return context.Response.WriteAsync(mensagem);
                        },

                        OnAuthenticationFailed = context =>
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var mensagem = context.Exception is SecurityTokenExpiredException
                                ? "Token expirado. Faça login novamente."
                                : "Token inválido.";
                            var resposta = JsonSerializer.Serialize(new
                            {
                                status = 401,
                                message = mensagem
                            });
                            return context.Response.WriteAsync(resposta);
                        },

                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var mensagem = JsonSerializer.Serialize(new
                            {
                                status = 403,
                                message = "Você não tem permissão para acessar este recurso."
                            });
                            return context.Response.WriteAsync(mensagem);
                        }
                    };
                });
        }
    }
}
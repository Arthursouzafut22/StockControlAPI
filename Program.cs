using ControleMercadoria.API.Configuration;
using ControleMercadoria.Application.Services.Auth;
using ControleMercadoria.Application.Services.Products;
using ControleMercadoria.Application.Services.Users;
using ControleMercadoria.Core.Validators.Auth;
using ControleMercadoria.Infrastructure.Repository.Generic;
using ControleMercadoria.Infrastructure.Repository.Products;
using ControleMercadoria.Infrastructure.Repository.Users;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.AddJwtConfiguration();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDTOValidator>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var secretKey = builder.Configuration["Jwt:SecretKey"];

if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("A chave JWT não foi configurada!");
}

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


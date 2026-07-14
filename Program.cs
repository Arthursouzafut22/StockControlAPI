using ControleMercadoria.API.Configuration;
using ControleMercadoria.Application.Services.Auth;
using ControleMercadoria.Application.Services.Movements;
using ControleMercadoria.Application.Services.Products;
using ControleMercadoria.Application.Services.Reports;
using ControleMercadoria.Application.Services.Users;
using ControleMercadoria.Core.Validators.Auth;
using ControleMercadoria.Infrastructure.Repository.Generic;
using ControleMercadoria.Infrastructure.Repository.Movements;
using ControleMercadoria.Infrastructure.Repository.Products;
using ControleMercadoria.Infrastructure.Repository.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => {

    options.AddPolicy("ReactPolicy", policy => {

        policy
        .WithOrigins("http://localhost:5173", "https://fluxo-mercadoria.vercel.app")
        .AllowAnyHeader()
        .AllowAnyMethod();
    
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.TagActionsBy(api =>
    {
        var controller = api.ActionDescriptor.RouteValues["controller"];

        return controller switch
        {
            "Products" => new[] { "Produtos" },
            "Movements" => new[] { "Movimentações" },
            "Reports" => new[] { "Relatórios" },
            "Auth" => new[] { "Autenticação" },
            "Users" => new[] { "Usuários" },
            _ => new[] { controller! }
        };
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token JWT."
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});


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
builder.Services.AddScoped<IMovementsRepository, MovementsRepository>();
builder.Services.AddScoped<IMovementService, MovementService>();
builder.Services.AddScoped<IReportsService, ReportsService>();

var secretKey = builder.Configuration["Jwt:SecretKey"];

if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("A chave JWT não foi configurada!");
}

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("ReactPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


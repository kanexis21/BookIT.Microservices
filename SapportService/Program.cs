using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using SapportService.Hubs;
using SupportService.Api.Infrastructure;
using SupportService.Application;
using SupportService.Core.Application.Common.Mapping;
using SupportService.Core.Application.Common.Services;
using SupportService.Core.Application.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IMessageDbContext).Assembly));
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5005";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
        // Чтобы SignalR мог читать токен из query string:
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // Если это подключение к хабу - подставляем токен
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/supporthub"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "support_api");
    });
});

builder.Services.AddApplication();
builder.Services.AddControllers();
// Инфраструктура: репозитории и т.д.
builder.Services.AddPersistense(builder.Configuration);

// Swagger (для тестирования API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("https://localhost:7179") 
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
    );
});

builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
builder.Services.AddSignalR();


var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<SupportHub>("/supporthub").AllowAnonymous();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

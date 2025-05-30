using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using RoomService.Api.Application;
using RoomService.Api.Core.Application.Common.Mapping;
using RoomService.Api.Core.Application.Interfaces;
using RoomService.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IRoomDbContext).Assembly));
});
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5005";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "room_api");
    });
});

builder.Services.AddApplication();
builder.Services.AddControllers();
// Инфраструктура: репозитории и т.д.
builder.Services.AddPersistense(builder.Configuration);

// Swagger (для тестирования API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS, если есть Web-клиент
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

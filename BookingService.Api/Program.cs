using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using BookingService.Api.Application.Common.Mapping;
using BookingService.Api.Core.Application;
using BookingService.Api.Core.Application.Interfaces;
using BookingService.Api.Infrastructure;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IBookingDbContext).Assembly));
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); 
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5005";
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateAudience = false,
            NameClaimType = "sub",
            RoleClaimType = "role"
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "booking_api");
    });
});
// CQRS: Application ���� (Handlers, Validators)
builder.Services.AddApplication();
builder.Services.AddControllers();
// ��������������: ����������� � �.�.
builder.Services.AddPersistense(builder.Configuration);

// Swagger (��� ������������ API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS, ���� ���� Web-������
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

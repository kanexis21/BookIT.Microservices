using System.Reflection;
using ResourceService.Api.Application;
using Microsoft.IdentityModel.Tokens;
using ResourceService.Api.Application.Common.Mapping;
using ResourceService.Api.Application.Interfaces;
using ResourceService.Api.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IResourceDbContext).Assembly));
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
        policy.RequireClaim("scope", "resource_api"); 
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

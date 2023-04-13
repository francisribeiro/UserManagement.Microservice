using UserManagement.Application.Services;
using UserManagement.Application.Contracts;
using UserManagement.Infrastructure.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddScoped<IValidationService, ValidationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
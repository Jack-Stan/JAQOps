using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JAQOps.Data;
using Hangfire;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JAQOps API", Version = "v1" });
    
    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configure database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<JAQOpsDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(builder.Configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:3000" })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"] ?? "DefaultSecretKeyForDevelopment"))
        };
    });

// Configure Hangfire
builder.Services.AddHangfire(config =>
    config.UseStorage(new MySqlStorage(
        connectionString, 
        new MySqlStorageOptions()
    )));

builder.Services.AddHangfireServer();

// Configure SignalR
builder.Services.AddSignalR();

// Register repositories
builder.Services.AddScoped<JAQOps.Data.Repositories.Interfaces.IRepository<JAQOps.Data.Entities.Tenant>, JAQOps.Data.Repositories.Repository<JAQOps.Data.Entities.Tenant>>();
builder.Services.AddScoped<JAQOps.Data.Repositories.Interfaces.ITenantRepository, JAQOps.Data.Repositories.TenantRepository>();
builder.Services.AddScoped<JAQOps.Data.Repositories.Interfaces.IRepository<JAQOps.Data.Entities.User>, JAQOps.Data.Repositories.Repository<JAQOps.Data.Entities.User>>();
builder.Services.AddScoped<JAQOps.Data.Repositories.Interfaces.IUserRepository, JAQOps.Data.Repositories.UserRepository>();

// Register services
builder.Services.AddScoped<JAQOps.Business.Interfaces.IAuthService, JAQOps.Business.Services.AuthService>();
builder.Services.AddScoped<JAQOps.Business.Interfaces.ITenantService, JAQOps.Business.Services.TenantService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Hangfire Dashboard
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    // Add authorization here
});

// SignalR hub
app.MapHub<JobHub>("/hubs/jobs");

app.MapControllers();

// Sample endpoint for testing
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// SignalR Hub for real-time job updates
public class JobHub : Hub
{
    public async Task JobStatusUpdated(Guid jobId, string status)
    {
        await Clients.All.SendAsync("ReceiveJobUpdate", jobId, status);
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SmartEvent.Core.Interfaces;
using SmartEvent.Data;
using SmartEvent.Data.Repositories;
using SmartEvent.Services;
using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartEvent API", Version = "v1" });
});

// Configure DbContext
builder.Services.AddDbContext<SmartEventDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register repositories - Core interfaces with Data implementations
builder.Services.AddScoped<SmartEvent.Core.Interfaces.IEventRepository, EventRepository>();
builder.Services.AddScoped<SmartEvent.Core.Interfaces.IAttendeeRepository, AttendeeRepository>();

// Register services
builder.Services.AddScoped<SmartEvent.Core.Interfaces.IEventService, EventService>();
builder.Services.AddScoped<SmartEvent.Core.Interfaces.IRegistrationService, RegistrationService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

// Seed the database with sample data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SmartEventDbContext>();
        // Make sure database is created
        context.Database.EnsureCreated();
        // Seed the data
        DataSeeder.SeedData(context).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();

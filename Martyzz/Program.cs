using Martyzz.Domain.Repo.Interfaces;
using Martyzz.Infrastructure.Data;
using Martyzz.Infrastructure.Repositories;
using Martyzz.Mappings;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(A => A.AddProfile(new MappingProfile()));

builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Apply pending migrations on startup
using var scope = app.Services.CreateScope();

var servicesProvider = scope.ServiceProvider; // Get the service provider from the scope
var context = servicesProvider.GetRequiredService<StoreDbContext>(); // Ask the provider for the StoreDbContext service

// add the logger as well with the same way

var loggerFactory = servicesProvider.GetRequiredService<ILoggerFactory>();

var logger = loggerFactory.CreateLogger<Program>();

try
{
    await context.Database.MigrateAsync();
    //StoreSeedData.SeedAsync(context);
    logger.LogInformation("Database migration applied successfully.");
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred while applying database migrations.");
}

// Add mapper services

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

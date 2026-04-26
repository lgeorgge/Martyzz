using Martyzz.Domain.Repo.Interfaces;
using Martyzz.Infrastructure.Data;
using Martyzz.Infrastructure.Repositories;
using Martyzz.Mappings;
using Martyzz.Mappings.Resolvers;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add logging
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Services.AddSerilog();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddTransient<ProductImageUrlResolver>();

// Mappers (register profile types so AutoMapper can resolve value resolvers from DI)
builder.Services.AddAutoMapper(O => O.AddProfile(new MappingProfile()));

// Basket
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// Caching
builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis") ?? "localhost")
);

// Database
builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// log starting
Log.Information("App is starting");

var app = builder.Build();

// Apply pending migrations on startup
using var scope = app.Services.CreateScope();

var servicesProvider = scope.ServiceProvider; // Get the service provider from the scope
var context = servicesProvider.GetRequiredService<StoreDbContext>(); // Ask the provider for the StoreDbContext service

try
{
    await context.Database.MigrateAsync();
    //StoreSeedData.SeedAsync(context);
    Log.Information("Database migration applied successfully.");
}
catch (Exception ex)
{
    Log.Error(ex, "An error occurred while applying database migrations.");
}

// Add mapper services

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();

using PCRE.Data;
using Scalar.AspNetCore; // New UI Library
// This line is the magic wand for PostgreSQL arrays
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Native .NET 9 OpenAPI support

// Register Repository
builder.Services.AddScoped<IPrognosticsRepository, PrognosticsRepository>();

// This registers our new factory so the Repository can find it
builder.Services.AddScoped<IDbConnectionFactory, NpgsqlConnectionFactory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Native .NET 9 OpenAPI Endpoint
    app.MapOpenApi(); 
    
    // Modern Interactive UI (Replacement for Swagger UI)
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
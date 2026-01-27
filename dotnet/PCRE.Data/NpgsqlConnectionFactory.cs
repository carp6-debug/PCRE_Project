using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace PCRE.Data;

public class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        // This pulls your 'PostgreSql' string from appsettings.json
        _connectionString = configuration.GetConnectionString("PostgreSql") 
            ?? throw new InvalidOperationException("Connection string 'PostgreSql' not found.");
    }

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}
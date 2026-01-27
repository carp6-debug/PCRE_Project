using Dapper;
using Npgsql;
using PCRE.Models;
using System.Data;
// IMPORTANT: Add the namespace where your EXISTING IDbConnectionFactory lives
using PCRE.Data; 

namespace PCRE.Data;

public class PrognosticsRepository : IPrognosticsRepository
{
    // We are using the EXACT variable names required for your existing DB setup
    private readonly IDbConnectionFactory _connectionFactory;

    public PrognosticsRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    // --- RETAINED: Your successful 200 OK JSON Bridge logic ---
    public async Task<IEnumerable<TransientReading>> GetCapacitorData(int capId, int limit)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"
            SELECT 
                reading_id AS ReadingId,
                serial_date AS SerialDate, 
                array_to_json(v_load)::text AS VLoad, 
                array_to_json(v_out)::text AS VOut 
            FROM prognostics.transient_readings 
            WHERE cap_id = @CapId 
            ORDER BY serial_date ASC
            LIMIT @Limit";

        return await connection.QueryAsync<TransientReading>(sql, new { CapId = capId, Limit = limit });
    }

    // --- NEW: The Threshold Search using the same JSON Bridge pattern ---
    public async Task<IEnumerable<TransientReading>> GetReadingsBelowThreshold(int capId, double threshold)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"
            SELECT * FROM (
                SELECT 
                    reading_id AS ReadingId,
                    serial_date AS SerialDate, 
                    array_to_json(v_load)::text AS VLoad, 
                    array_to_json(v_out)::text AS VOut,
                    (SELECT AVG(val) FROM unnest(v_out) AS val) AS ComputedAvg
                FROM prognostics.transient_readings 
                WHERE cap_id = @CapId
            ) AS sub
            WHERE ComputedAvg < @Threshold
            ORDER BY SerialDate ASC";

        return await connection.QueryAsync<TransientReading>(sql, new { CapId = capId, Threshold = threshold });
    }

    public async Task<TransientReading?> GetLatestStatus(int capId)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"
            SELECT 
                reading_id AS ReadingId,
                serial_date AS SerialDate, 
                array_to_json(v_out)::text AS VOut 
            FROM prognostics.transient_readings 
            WHERE cap_id = @CapId 
            ORDER BY serial_date DESC 
            LIMIT 1";

        return await connection.QueryFirstOrDefaultAsync<TransientReading>(sql, new { CapId = capId });
    }
}
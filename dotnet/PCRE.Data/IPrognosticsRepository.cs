using PCRE.Models;

namespace PCRE.Data
{
    /// <summary>
    /// Contract for accessing NASA PCRE telemetry and health analytics.
    /// </summary>
    public interface IPrognosticsRepository
    {
        // Fetches a window of history for the Python analysis plots
        Task<IEnumerable<TransientReading>> GetCapacitorData(int capId, int limit = 100);
        
        // Server-side filtering for anomaly detection alerts
        Task<IEnumerable<TransientReading>> GetReadingsBelowThreshold(int capId, double threshold);

        // Snapshot of the most recent health status for the dashboard
        Task<TransientReading?> GetLatestStatus(int capId);
    }
}

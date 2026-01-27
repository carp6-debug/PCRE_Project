using System.Text.Json;
using System.Linq;

namespace PCRE.Models;

public class TransientReading
{
    public long ReadingId { get; set; }
    public double SerialDate { get; set; }
    public string? VLoad { get; set; } 
    public string? VOut { get; set; }

    // Replicates your Python matlab_to_datetime logic
    public DateTime HumanDate => DateTime.Parse("0001-01-01").AddDays(SerialDate - 367);

    public double AvgVoltage
    {
        get
        {
            if (string.IsNullOrEmpty(VOut)) return 0;
            // Handle those "NoneType artifacts" defensively
            var list = JsonSerializer.Deserialize<List<double?>>(VOut);
            var cleanValues = list?.Where(v => v.HasValue).Select(v => v!.Value).ToList();
            return cleanValues != null && cleanValues.Count > 0 ? cleanValues.Average() : 0;
        }
    }
    
    // NEW: Analytical properties for the "Fleet Status" view
    public double HealthIndex => Math.Min(1.0, AvgVoltage / 4.8); // Normalizing to 4.8V baseline

    public string Status => HealthIndex switch
    {
        >= 0.95 => "Nominal",
        >= 0.85 => "Degraded",
        _ => "Critical"
    };
}
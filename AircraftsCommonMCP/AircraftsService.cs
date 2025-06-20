using System.Text.Json;
using System.Text.Json.Serialization;

namespace AircraftsCommonMCP;

public class AircraftsService
{
    private readonly List<Aircraft> aircrafts;

    public AircraftsService()
    {
        var assembly = typeof(AircraftsService).Assembly;
        using var stream = assembly.GetManifestResourceStream("AircraftsCommonMCP.aircrafts.json");
        using var reader = new StreamReader(stream!);
        var json = reader.ReadToEnd();
        aircrafts = JsonSerializer.Deserialize(json, AircraftsContext.Default.ListAircraft) ?? new List<Aircraft>();
    }

    List<Aircraft> aircraftsList = new();
    public async Task<List<Aircraft>> GetAircrafts()
    {
        await Task.Run(() => { });

        return aircrafts;
    }

    public async Task<List<Aircraft>> GetAircraftsByFamily(string family)
    {
        await Task.Run(() => { });

        return aircrafts.Where(c => c.Families != null && c.Families.Contains(family, StringComparer.OrdinalIgnoreCase)).ToList();
    }

    public async Task<Aircraft?> GetAircraft(string name)
    {
        await Task.Run(() => { });

        return aircrafts.FirstOrDefault(m => m.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) == true);
    }
}

public partial class Aircraft
{
    public string? Name { get; set; } 
    public string? Hexcode { get; set; }   
    public string? RGB { get; set; }   
    public List<string>? Families { get; set; }  
}

[JsonSerializable(typeof(List<Aircraft>))]
[JsonSerializable(typeof(Aircraft))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class AircraftsContext : JsonSerializerContext
{
}

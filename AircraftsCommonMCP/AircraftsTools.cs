using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace AircraftsCommonMCP;

[McpServerToolType]
public sealed class AircraftsTools
{
    private readonly AircraftsService aircraftsService;

    public AircraftsTools(AircraftsService aircraftsService)
    {
        this.aircraftsService = aircraftsService;
    }

    [McpServerTool, Description(AircraftsInfo.GetAllAircraftsToolDescription)]
    public async Task<string> GetAllAircrafts()
    {
        var aircrafts = await aircraftsService.GetAircrafts();
        return JsonSerializer.Serialize(aircrafts, AircraftsContext.Default.ListAircraft);
    }

    [McpServerTool, Description(AircraftsInfo.GetAircraftsByFamilyToolDescription)]    
    public async Task<string> GetAircraftByFamily([Description(AircraftsInfo.GetAircraftsByFamilyParamFamilyDescription)] string family)
    {
        var aircrafts = await aircraftsService.GetAircraftsByFamily(family);
        return JsonSerializer.Serialize(aircrafts, AircraftsContext.Default.ListAircraft);
    }


    [McpServerTool, Description(AircraftsInfo.GetAircraftToolDescription)]
    public async Task<string> GetAircraft([Description(AircraftsInfo.GetAircraftParamNameDescription)] string name)
    {
        var aircraft = await aircraftsService.GetAircraft(name);
        return JsonSerializer.Serialize(aircraft, AircraftsContext.Default.Aircraft);
    }
}

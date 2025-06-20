using AircraftsCommonMCP;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AircraftsMCPSSE_func
{
    public class AircraftsToolsFunc
    {
        private readonly ILogger<AircraftsToolsFunc> _logger;
        private readonly AircraftsService _aircraftsService;

        public AircraftsToolsFunc(ILogger<AircraftsToolsFunc> logger, AircraftsService aircraftsService)
        {
            _logger = logger;
            _aircraftsService = aircraftsService;
        }

        [Function(AircraftsInfo.GetAllAircraftsToolName)]
        public async Task<string> GetAllAircrafts(
            [McpToolTrigger(AircraftsInfo.GetAllAircraftsToolName, AircraftsInfo.GetAllAircraftsToolDescription)]
            ToolInvocationContext context
        )
        {
            var aircrafts = await _aircraftsService.GetAircrafts();
            return JsonSerializer.Serialize(aircrafts);
        }

        [Function(AircraftsInfo.GetAircraftsByFamilyToolName)]
        public async Task<string> GetAircraftsByFamily(
            [McpToolTrigger(AircraftsInfo.GetAircraftsByFamilyToolName, AircraftsInfo.GetAircraftsByFamilyToolDescription)]
            ToolInvocationContext context,
            [McpToolProperty("Name", "string", AircraftsInfo.GetAircraftsByFamilyParamFamilyDescription)]
            string family
        )
        {
            var aircrafts = await _aircraftsService.GetAircraftsByFamily(family);
            return JsonSerializer.Serialize(aircrafts);
        }

        [Function(AircraftsInfo.GetAircraftToolName)]
        public async Task<string> GetAircraft(
            [McpToolTrigger(AircraftsInfo.GetAircraftToolName, AircraftsInfo.GetAircraftToolDescription)]
            ToolInvocationContext context,
            [McpToolProperty("Name", "string", AircraftsInfo.GetAircraftParamNameDescription)]
            string name
        )
        {
            var aircraft = await _aircraftsService.GetAircraft(name);
            return JsonSerializer.Serialize(aircraft);
        }
    }
}

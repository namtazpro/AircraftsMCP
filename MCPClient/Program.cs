using ModelContextProtocol.Client;

namespace MCPClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("*** MCP Client ***");

            var clientTransport = new SseClientTransport(new SseClientTransportOptions
            {
                Name = "AircraftsMCP",
                Endpoint = new Uri("http://localhost:3000/")
            });


            var client = await McpClientFactory.CreateAsync(clientTransport);

            foreach (var tool in await client.ListToolsAsync())
            {
                Console.WriteLine($"{tool.Name} ({tool.Description})");
            }
            Console.WriteLine($"---");

            var aircraftNames = new[] { "Boeing737", "AirbusA320", "Cessna172", "F16", "Concorde" };
            foreach (var aircraftName in aircraftNames)
            {
                var result = await client.CallToolAsync(
                    "GetAircraft",
                    new Dictionary<string, object?>() { ["name"] = aircraftName },
                    cancellationToken: CancellationToken.None);

                Console.WriteLine($"{aircraftName}: {result.Content.First(c => c.Type == "text").Text}");
            }
        }
    }
}

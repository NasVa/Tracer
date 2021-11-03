using System.Text.Json;
using Tracer1;

namespace Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            return System.Text.Json.JsonSerializer.Serialize(traceResult.Threads, options);
        }
    }
}

using Tracer1;

namespace Serialization
{
    public interface ISerializer
    {
        public string Serialize(TraceResult traceResult);
    }
}

using Tracer1;
using Serialization;


namespace Writer
{
    interface IWriter
    {
        public void Write(TraceResult result, ISerializer serializer);
    }
}

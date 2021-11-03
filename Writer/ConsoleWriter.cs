using System;
using Tracer1;
using Serialization;

namespace Writer
{
    public class ConsoleWriter : IWriter
    {
        public void Write(TraceResult result, ISerializer serializer)
        {
            Console.WriteLine(serializer.Serialize(result));
        }
    }
}

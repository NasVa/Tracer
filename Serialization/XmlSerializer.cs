using Tracer1;
using System.IO;

namespace Serialization
{
    public class XmlSerializer : ISerializer
    {
        private static readonly System.Xml.Serialization.XmlSerializer Serializer = new System.Xml.Serialization.XmlSerializer(typeof(TraceResult));

        public string Serialize(TraceResult traceResult)
        {
            var sw = new StringWriter();
            Serializer.Serialize(sw, traceResult);
            return sw.ToString();
        }
    }
}

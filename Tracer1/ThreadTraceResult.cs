using System.Collections.Immutable;
using System.Xml.Serialization;

namespace Tracer1
{
    [XmlType("thread")]
    public class ThreadTraceResult
    {
        [XmlAttribute]
        public int ThreadId { get; set; }
        [XmlAttribute]
        public long Time { get; set; }
        public ImmutableList<MethodInfo> Methods { get; set; }

        public ThreadTraceResult() { }

        public ThreadTraceResult(int ThreadId)
        {
            this.ThreadId = ThreadId;
            Time = 0;
            Methods = ImmutableList<MethodInfo>.Empty;
        }
        internal void AddMethod(MethodInfo method)
        {
            Methods = Methods.Add(method);
            Time = Time + method.Time;
        }
    }
}

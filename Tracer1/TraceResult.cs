using System.Collections.Generic;
using System.Collections.Immutable;
using System.Xml.Serialization;

namespace Tracer1
{
    [XmlType("root")]
    public class TraceResult
    {
        private readonly ImmutableList<ThreadTraceResult> _threads;

        public ImmutableList<ThreadTraceResult> Threads => _threads;

        public TraceResult() { }

        public TraceResult(List<ThreadTraceResult> threadTraceResults)
        {
            _threads = ImmutableList.CreateRange(threadTraceResults);
        }
    }
}
using System.Linq;
using System.Collections.Concurrent;
using System.Threading;

namespace Tracer1
{
    public class Tracer : ITracer
    {

        private ConcurrentDictionary<int, SingleThreadTracer> SingleThreadTracers = new ConcurrentDictionary<int, SingleThreadTracer>();

        public void StartTrace()
        {
            int id = Thread.CurrentThread.ManagedThreadId;

            if (!SingleThreadTracers.ContainsKey(id))
            {
                SingleThreadTracers.TryAdd(id, new SingleThreadTracer(id));
            }
            SingleThreadTracers[id].StartTrace();
        }


        public void StopTrace()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            SingleThreadTracers[id].EndTrace();
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(SingleThreadTracers.Values.Select(Tracer => Tracer.ThreadTraceResult).ToList());
        }
    }
}
using System;
using System.Threading;
using Tracer1;
using Serialization;

namespace Writer
{
    class Foo {
        private ITracer tracer;
        public Foo(ITracer tracer)
        {
            this.tracer = tracer;
        } 
        public void foo()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }
    }
    
    class Bar
    {
        private ITracer tracer;
        public Bar(ITracer tracer)
        {
            this.tracer = tracer;
        }
        public void bar()
        {
            tracer.StartTrace();
            Thread thread = new Thread(() =>
            {
                tracer.StartTrace();
                Thread thread = new Thread(() =>
                {
                    tracer.StartTrace();
                    Thread.Sleep(400);
                    tracer.StopTrace();
                });
                thread.Start();
                thread.Join();
                tracer.StopTrace();
            });
            thread.Start();
            thread.Join();
            tracer.StopTrace();
        }
    }
    
    class _Main
    {
        public static void Main(String[] args)
        {
            IWriter[] writers = new IWriter[3] { new FileWriter("jsonResult.json"), new FileWriter("xmlResult.xml"), new ConsoleWriter() };
            ITracer tracer = new Tracer();
            Foo foo = new Foo(tracer);
            Bar bar = new Bar(tracer);
            foo.foo();
            bar.bar();
            TraceResult traceResult = tracer.GetTraceResult();
            writers[0].Write(traceResult, new JsonSerializer());
            writers[1].Write(traceResult, new XmlSerializer());
            writers[2].Write(traceResult, new XmlSerializer());
        }
    }
}

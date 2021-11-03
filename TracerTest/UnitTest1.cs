using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tracer1;

namespace TracerTest
{
    [TestClass]
    public class TracerTest
    {
        private ITracer _tracer;
        private Foo _foo;

        [TestInitialize]
        public void Initialize()
        {
            _tracer = new Tracer1.Tracer();
            _foo = new Foo(_tracer);
        }

        [TestMethod]
        public void SingleThreadTest()
        {
            _foo.MyMethod();
            var result = _tracer.GetTraceResult();
            Assert.AreEqual(1, result.Threads.Count);
        }

        [TestMethod]
        public void InnerThreadCreationTest()
        {
            _foo.MyMethodWithThreads();
            var result = _tracer.GetTraceResult();
            Assert.AreEqual(2, result.Threads.Count);
        }

        [TestMethod]
        public void MultipleThreadsTest()
        {
            Thread t1 = new Thread(_foo.MyMethodWithThreads);
            Thread t2 = new Thread(_foo.MyMethodWithThreads);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            var result = _tracer.GetTraceResult();
            Assert.AreEqual(4, result.Threads.Count);
        }
    }

    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        public ITracer Tracer => _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            _bar.InnerMethod();
            _tracer.StopTrace();
        }

        public void MyMethodWithThreads()
        {
            _tracer.StartTrace();
            Thread thread = new Thread(_bar.InnerMethod);
            thread.Start();
            thread.Join();
            Thread.Sleep(10);
            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(20);
            _tracer.StopTrace();
        }
    }
}
using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Tracer1;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TracerTest
{
    public class Foo
    {
        public Bar bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            bar = new Bar(_tracer);
        }

        public void MyMethod1()           //метод в методе
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            bar.MyMethod2();
            _tracer.StopTrace();
        }

        public void MyMethod3()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            Thread tr3 = new Thread(bar.MyMethod2);
            tr3.Start();
            tr3.Join();
            _tracer.StopTrace();
        }
        
        public void MyMethod4()      //метод
        {
            _tracer.StartTrace();
            Thread.Sleep(300);
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

        public void MyMethod2()     
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _tracer.StopTrace();
        }
    }
    
    public class Tests
    {
        private ITracer tracer;
        private Foo foo;
        
        [SetUp]
        public void Setup()
        {
            tracer = new Tracer();
            foo = new Foo(tracer);
        }

        [Test]
        public void MethodDataTest()                       //на метод и класс
        {
            foo.bar.MyMethod2();
            TraceResult result = tracer.GetTraceResult();
            Assert.AreEqual("MyMethod2", result.Threads[0].Methods[0].MethodName);
            Assert.AreEqual("Bar", result.Threads[0].Methods[0].ClassName);
            Console.WriteLine("MethodDataTest");
            Console.WriteLine(result.Threads[0].Methods[0].Time);
            
        }
        
        [Test]
        public void InnerMethodTest()                 //на метод в методе
        {
            foo.MyMethod1();
            TraceResult result = tracer.GetTraceResult();
            Assert.AreEqual("MyMethod1", result.Threads[0].Methods[0].MethodName);
            Assert.AreEqual("MyMethod2", result.Threads[0].Methods[0].methods[0].MethodName);
            Assert.AreEqual("Foo", result.Threads[0].Methods[0].ClassName);
            Assert.AreEqual(1, result.Threads[0].Methods.Count);
            Assert.AreEqual(1, result.Threads[0].Methods[0].methods.Count);
            Console.WriteLine("InnerMethodTest");
            Console.WriteLine(result.Threads[0].Methods[0].Time);
            Console.WriteLine(result.Threads[0].Methods[0].methods[0].Time);
        }
        

        [Test]
        public void ThreadsTest()
        {
            Thread t1 = new Thread(foo.MyMethod1);
            Thread t2 = new Thread(foo.MyMethod4);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            TraceResult result = tracer.GetTraceResult();
            Assert.AreEqual(2, result.Threads.Count);
            Console.WriteLine(result.Threads[0].Methods[0].Time);
            Console.WriteLine(result.Threads[1].Methods[0].Time);
        }
        
    }
}
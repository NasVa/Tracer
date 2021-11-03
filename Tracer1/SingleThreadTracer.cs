using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tracer1
{
    public class SingleThreadTracer
    {

        public ThreadTraceResult ThreadTraceResult { get; private set; }
        private Stack<MethodInfo> MethodCallStack;

        internal SingleThreadTracer(int id)
        {
            ThreadTraceResult = new ThreadTraceResult(id);
            MethodCallStack = new Stack<MethodInfo>();
        }

        public void StartTrace()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(2).GetMethod();
            string methodName = method.Name;
            string className = method.DeclaringType.Name;
            long now = DateTime.Now.Ticks;
            var methodInfo = new MethodInfo(className, methodName, now);
            MethodCallStack.Push(methodInfo);
        }

        public void EndTrace()
        {
            long now = DateTime.Now.Ticks;
            if (MethodCallStack.Count == 0)
            {
                throw new Exception("Error. Calling End Trace without starting");
            }
            var currentMethodInfo = MethodCallStack.Pop();
            currentMethodInfo.Time = (now - currentMethodInfo.Time) / 10000;
            if (MethodCallStack.Count == 0)
            {
                ThreadTraceResult.AddMethod(currentMethodInfo);
            } 
            else
            {
                MethodCallStack.Peek().methods = MethodCallStack.Peek().methods.Add(currentMethodInfo);
            }
        }
    }
}

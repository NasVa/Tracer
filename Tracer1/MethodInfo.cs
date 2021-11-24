using System.Collections.Immutable;
using System.Xml.Serialization;

namespace Tracer1
{
    [XmlType("method")]
    public class MethodInfo
    {
        [XmlAttribute]
        public long Time { get; set; }
        
        [XmlAttribute]
        public string MethodName { get; set; }
        
        [XmlAttribute]
        public string ClassName { get; set; }
        public ImmutableList<MethodInfo> methods { get; internal set;  } 

        public MethodInfo() { }

        public MethodInfo(string ClassName, string MethodName, long time)
        {
            this.Time = time;
            this.MethodName = MethodName;
            this.ClassName = ClassName;
            this.methods = ImmutableList<MethodInfo>.Empty;
        }
    }
}

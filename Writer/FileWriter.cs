using Serialization;
using System.IO;
using System;
using Tracer1;

namespace Writer
{
    class FileWriter : IWriter
    {
        private string Path;

        public FileWriter(string Path)
        {
            this.Path = Path;
        }

        public void Write(TraceResult result, ISerializer serializer)
        {
            String output = serializer.Serialize(result);
            File.WriteAllTextAsync(Path, output);
        }
    }
}

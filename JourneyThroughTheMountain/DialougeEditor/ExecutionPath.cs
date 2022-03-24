using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DialougeEditor
{
    public class ExecutionPath : ISerializable
    {

        public bool IsSignaled { get; set; }

        public void Signal()
        {
            IsSignaled = true;
        }

        public ExecutionPath()
        {

        }

        private ExecutionPath(SerializationInfo info, StreamingContext ctx)
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialougeEditor
{
    internal class SocketVisual
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public bool Input { get; set; }
        public object Value { get; set; }
        public bool IsMainExecution { get; set; }

        public bool IsExecution
        {
            get { return Type.Name.Replace("&", "") == typeof (Exe)}
        }
    }
}

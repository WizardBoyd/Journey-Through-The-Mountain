using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialougeEditor
{
    public class NodeVisual
    {
        public const float NodeWidth = 140;
        public const float HeaderHeight = 20;
        public const float ComponentPadding = 2;

        /// <summary>
        /// Current Name of Node
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current Node X position
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// Current Y position of Node
        /// </summary>
        public float Y { get; set; }
        internal MethodInfo Type { get; set; }
        internal int Order { get; set; }
        internal bool Callable { get; set; }
        internal bool ExecInit { get; set; }

        internal bool IsSlected { get; set; }
        internal FeedBackType Feedback { get; set; }
        private object nodeContext { get; set; }
        public Control CustomEditor { get; internal set; }
        internal string GUID = Guid.NewGuid().ToString();
        internal Color NodeColor = Color.Blue;
        public bool IsBackExecuted { get; internal set; }

        private //socket
    }
}

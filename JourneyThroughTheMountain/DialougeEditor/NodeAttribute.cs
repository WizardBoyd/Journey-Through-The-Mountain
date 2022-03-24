using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialougeEditor
{
    /// <summary>
    /// A class used for the nodes attributes
    /// </summary>
    public class NodeAttribute : Attribute
    {
        private const int Auto = -1;
        //public string UniqueID { get; set; }

        //public string NPCMessage { get; set; }

        public string Menu { get; set; }

        public string Category { get; set; }

        public bool IsCallable { get; set;}

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsExecutionInitiator { get; set; }

        public Type CustomEditor { get; set; }

        public string XmlExportName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public NodeAttribute(string name = "Node", string menu = "", string category = "General",
            string description = "some node.", bool isCallable = true, bool isExecutionInitiator = false, Type customEditor = null,
            string xmlExportName = "", int width = Auto, int height = Auto)
        {
            Name = name;
            Menu = menu;
            Category = category;
            Description = description;
            IsCallable = isCallable;
            IsExecutionInitiator = isExecutionInitiator;
            CustomEditor = CustomEditor;
            XmlExportName = xmlExportName;
            Width = width;
            Height = height;
        }

        public string Path
        {
            get { return Menu + "/" + Name; }
        }
    }
}

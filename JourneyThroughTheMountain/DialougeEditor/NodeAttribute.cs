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
    public class NodeAttribute: Attribute
    {
        public string UniqueID { get; set; }

        public string NPCMessage { get; set; }
    }
}

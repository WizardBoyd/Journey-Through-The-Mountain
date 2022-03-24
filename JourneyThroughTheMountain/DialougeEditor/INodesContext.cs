using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialougeEditor
{
    public interface INodesContext
    {
        /// <summary>
        /// property that is set to the actual currently processing node
        /// </summary>
        NodeVisual CurrentProcessingNode { get; set; }

        /// <summary>
        /// Event that can be raised when the application returns some feedback information
        /// </summary>
        event Action<string, NodeVisual, FeedBackType, object, bool> FeedbackInfo;
    }
}

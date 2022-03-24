﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialougeEditor
{
    class DialougeContext : INodesContext
    {
        public NodeVisual CurrentProcessingNode { get; set; }

        public event Action<string, NodeVisual, FeedBackType, object, bool> FeedbackInfo;

        [Node("Test", "Testing", "In-Development", "Just a Test Node", false)]
        public void InputString(string InValue, out string outValue)
        {
            outValue = InValue;
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialougeEditor
{
    [TypeConverter(typeof(DynamicNodeContextConverter))]
    public class DialougeChoiceNode: TextBox, IZoomable
    {


        public float zoom = 1f;
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
            }
        }

        DialougeChoiceNode()
        {
            Invalidated += UpdateNodeContext;
            
        }

        ~DialougeChoiceNode()
        {
            Invalidated -= UpdateNodeContext;
        }

        private void UpdateNodeContext(object sender, EventArgs e)
        {
            var node = (Tag as NodeVisual);
            if (node != null)
            {
                dynamic context = node.getNodeContext();
                //ADD context.What I want to store
            }
        }
    }
}

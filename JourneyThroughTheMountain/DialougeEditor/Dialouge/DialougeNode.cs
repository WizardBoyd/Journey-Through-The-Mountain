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
    public class DialougeNode : TextBox
    {


        public DialougeNode()
        {
            TextChanged += UpdateNodeContext;
        }

        private void UpdateNodeContext(object sender,EventArgs e)
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

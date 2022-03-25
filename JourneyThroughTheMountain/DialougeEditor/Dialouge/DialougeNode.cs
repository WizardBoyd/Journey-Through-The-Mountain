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
        private string _name;

        public string Name
        {
            get
            {
                return Text;
            }
            set
            {
                 
            }
        }

        public DialougeNode()
        {
            //Need some sort of event
        }

        private void UpdateNodeContext(object sender,MouseEventArgs e)
        {
            var node = (Tag as NodeVisual);
            if (node != null)
            {
                dynamic context = node.getNodeContext();
                context.Name = Name;
            }
        }

    }
}

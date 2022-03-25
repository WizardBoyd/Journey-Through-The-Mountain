using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialougeEditor
{
    public partial class DialougeForm : Form
    {

        DialougeContext context = new DialougeContext();

        public DialougeForm()
        {
            InitializeComponent();
        }

        private void DialougeForm_Load(object sender, EventArgs e)
        {
            nodeEditor.Context = context;
            nodeEditor.OnNodeContextSelected += NodesControlOnNodeContextSlected;
            
        }

        private void NodesControlOnNodeContextSlected(object o)
        {

            PropertyGrid.SelectedObject = o;
        }
    }
}

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
    public partial class DialogueControl : UserControl
    {

        public DialogueControl()
        {
            InitializeComponent();
        }


        protected void Test(object sender, EventArgs e)
        {
            string test = "";
        }

        protected void PlusButton_Click(object sender, EventArgs e)
        {
            TextBox T = new TextBox();
            T.Name = "Choice" + flowLayoutPanel.Controls.Count.ToString();
            flowLayoutPanel.Controls.Add(T);
            
        }

        protected void MinusBox_Click(object sender, EventArgs e)
        {
            if (!(flowLayoutPanel.Controls.Count - 1 < 0))
            {
                flowLayoutPanel.Controls.RemoveAt(flowLayoutPanel.Controls.Count - 1);
            }
        }
    }
}

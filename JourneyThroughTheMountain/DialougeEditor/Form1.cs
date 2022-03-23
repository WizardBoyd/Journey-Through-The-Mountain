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
    public partial class DialougeEditor : Form
    {
        public DialougeEditor()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void createNewDialougeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ContextMenu_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    ContextMenu.Hide();
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    ContextMenu.Show(this, new Point(e.X, e.Y));
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
        }

        private void ContextMenu_MouseDown(object sender, MouseEventArgs e)
        {
          
        }

        private void createBasicNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialougeEditorWindow.Controls.Add(new Dialouge.DialogueNode()); // Add Dailouge Node during runtime
        }

        private void DialougeEditor_Load(object sender, EventArgs e)
        {

        }
    }
}

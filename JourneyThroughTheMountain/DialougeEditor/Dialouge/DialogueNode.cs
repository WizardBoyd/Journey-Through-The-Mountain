using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DialougeEditor.Dialouge
{
    public class DialogueNode: Control
    {
        public string UniqueID;
        public List<DialogueNode> ChildNodes;
        public DialogueNode ParentNode;
        public string DialougeText;

        //WindowsForm Stuff
        public Label UniqueIDlabel;
        public RichTextBox DialougeTextBox;
        public Button XButton;
        public Button LinkButton;
        public Button PlusButton;


        public DialogueNode()
        {
            Location = MousePosition;
            UniqueIDlabel = new Label();
            DialougeTextBox = new RichTextBox();
            XButton = new Button();
            LinkButton = new Button();
            PlusButton = new Button();

            //Parenting
            UniqueIDlabel.Parent = this;
            DialougeTextBox.Parent = this;
            XButton.Parent = this;
            LinkButton.Parent = this;
            PlusButton.Parent = this;

            //Docking
            UniqueIDlabel.Dock = DockStyle.Top;
            DialougeTextBox.Dock = DockStyle.Fill;
            XButton.Dock = DockStyle.Bottom;
            LinkButton.Dock = DockStyle.Bottom;
            PlusButton.Dock = DockStyle.Bottom;

            //size
            this.Size = new Size(500, 500);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DialougeEditor.Dialouge
{
    public class DialogueNode: TableLayoutPanel
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


            this.RowStyles.Clear();
            this.ColumnStyles.Clear();

            this.ColumnCount = 3;

            for (int x = 1; x <= 4; x++)
            {
                this.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
            }

       
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
            



            //Parenting
            
            UniqueIDlabel.Parent = this;
            DialougeTextBox.Parent = this;
            XButton.Parent = this;
            LinkButton.Parent = this;
            PlusButton.Parent = this;

            this.Controls.Add(UniqueIDlabel, 0,0);
            this.Controls.Add(DialougeTextBox,0,1);
            this.Controls.Add(XButton,0,3);
            this.Controls.Add(LinkButton,1,3);
            this.Controls.Add(PlusButton, 2,3);



            //Inital Text
            XButton.Text = "X";
            LinkButton.Text = "Link";
            PlusButton.Text = "+";

            //Docking

            this.BackColor = Color.Gray;

        

            PlusButton.ForeColor = Color.Red;
            XButton.ForeColor = Color.Red;
            LinkButton.ForeColor = Color.Red;


            this.SetColumnSpan(UniqueIDlabel, 3);
            this.SetColumnSpan(DialougeTextBox, 3);

            this.Size = new Size(300, 300);
            this.AutoSize = true;
            for (int i = 0; i < this.Controls.Count; i++)
            {
                this.Controls[i].BackColor = Color.White;
                this.Controls[i].Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
                this.Controls[i].Height = this.Controls[i].Size.Height / 2;
            }

       



        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            //this.Size = DefaultSize;
            //this.Location = MousePosition;
            base.OnParentChanged(e);
        }

    }
}

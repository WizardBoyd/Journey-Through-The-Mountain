using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace DialougeEditor
{
    public partial class DialougeForm : Form
    {

        DialougeContext context = new DialougeContext();
        protected Point ClickPosition;
        protected Point ScrollPosition;
        protected Point LastPosition;

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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((stream = saveFileDialog.OpenFile()) != null)
                    {
                        using(BinaryWriter bw = new BinaryWriter(stream))
                        {
                            bw.Write(nodeEditor.Serialize());
                        }

                        stream.Close();
                    }
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream;
            using(OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((stream = openFileDialog.OpenFile()) != null )
                    {
                        using (MemoryStream br = new MemoryStream())
                        {
                            stream.CopyTo(br);
                            nodeEditor.Deserialize(br.ToArray());
                        }
                    }
                }
            }
        }

        private void exportToXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(Path.GetFullPath(saveFileDialog.FileName)+".xml", nodeEditor.ExportToXml());
                }
            }
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            nodeEditor.Execute();
        }

        private void nodeEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                ClickPosition.X = e.X;
                ClickPosition.Y = e.Y;
            }
       
        }

        private void nodeEditor_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                LastPosition.X = AutoScrollPosition.X;
                LastPosition.Y = AutoScrollPosition.Y;
            }
        }

        private void nodeEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                ScrollPosition.X = ClickPosition.X - e.X - LastPosition.X;
                ScrollPosition.Y = ClickPosition.Y - e.Y - LastPosition.Y;
                AutoScrollPosition = ScrollPosition;
            }
        }
    }
}

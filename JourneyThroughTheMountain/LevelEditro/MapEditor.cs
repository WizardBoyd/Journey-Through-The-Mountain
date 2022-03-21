using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using TileEngine;
namespace LevelEditro
{
    public partial class MapEditor : Form
    {
        public MapEditor()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            EditorWindow.DrawLayer = 1;

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //game.Exit();
            Application.Exit();
        }

        private void LoadImageList()
        {
            string filepath = Application.StartupPath + @"\RawContent\Tileset.png";

            Bitmap tileSheet = new Bitmap(filepath);

            int tilecount = 0;

            for (int y = 0; y < tileSheet.Height/ TileMap.TileHeight; y++)
            {
                for (int x = 0; x < tileSheet.Width / TileMap.TileWidth; x++)
                {
                    Bitmap newBitMap = tileSheet.Clone(new System.Drawing.Rectangle(x * TileMap.TileWidth, y * TileMap.TileHeight, TileMap.TileWidth, TileMap.TileHeight),
                        System.Drawing.Imaging.PixelFormat.DontCare);

                    imgListTiles.Images.Add(newBitMap);
                    string ItemName = "";
                    if (tilecount == 0)
                    {
                        ItemName = "Empty";
                    }
                    if (tilecount == 1)
                    {
                        ItemName = "White";
                    }
                    listTiles.Items.Add(new ListViewItem(ItemName, tilecount++));
                }
            }

            FixScrollBarScales();
        }

        //private void LoadObject()
        //{
        //    OpenFileDialog fileDialog = new OpenFileDialog();
        //    fileDialog.Multiselect = true;
        //    fileDialog.Filter = "Image files|*.png;*.jpeg;*.tiff";

        //    if (fileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        int tilecount = 0;

        //        foreach (string file in fileDialog.FileNames)
        //        {
        //            try
        //            {
        //                Bitmap image = new Bitmap(file);
        //                lstObjects.LargeImageList.Images.Add(image);
        //                lstObjects.Items.Add(new ListViewItem("", tilecount));
        //                tilecount++;
        //            }
        //            catch (Exception)
        //            {
        //                LstDebugBox.Items.Add($"{file} Could not be loaded");
        //            }
        //        }
        //    }
        //}

        private void MapEditor_Load(object sender, EventArgs e)
        {
            LoadImageList();
            FixScrollBarScales();
            cboCodeValues.Items.Clear();
            cboCodeValues.Items.Add("Mountain");
            cboCodeValues.Items.Add("Enemy");
            cboCodeValues.Items.Add("Lethal");
            cboCodeValues.Items.Add("EnemyBlocking");
            cboCodeValues.Items.Add("Start");
            cboCodeValues.Items.Add("Clear");
            cboCodeValues.Items.Add("Custom");
            cboCodeValues.Items.Add("Tree");
            cboCodeValues.Items.Add("Location");

            for (int x = 0; x < 100; x++)
            {
                cboMapNumber.Items.Add(x.ToString().PadLeft(3, '0'));
            }

            cboMapNumber.SelectedIndex = 0;
            TileMap.EditorMode = true;
            backgroundToolStripMenuItem.Checked = true;
            
        }

        private void FixScrollBarScales()
        {
            Camera.ViewPortWidth = EditorWindow.Width;
            Camera.ViewPortHeight = EditorWindow.Height;
            Camera.WorldRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, TileMap.TileWidth * TileMap.MapWidth, TileMap.TileHeight * TileMap.MapHeight);

            Camera.Move(Vector2.Zero);

            vScrollBar1.Minimum = 0;
            vScrollBar1.Maximum = Camera.WorldRectangle.Height - Camera.ViewPortHeight;

            hScrollBar1.Minimum = 0;
            hScrollBar1.Maximum = Camera.WorldRectangle.Width - Camera.ViewPortWidth;
        }

        private void MapEditor_Resize(object sender, EventArgs e)
        {
            FixScrollBarScales();
            
        }

        private void radioPassable_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPassable.Checked)
            {
                EditorWindow.EditingCode = false;
            }
            else
            {
                EditorWindow.EditingCode = true;
            }
        }

        private void cboCodeValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNewCode.Enabled = false;
            switch (cboCodeValues.Items[cboCodeValues.SelectedIndex].ToString())
            {
                case "Mountain":
                    txtNewCode.Text = "Mountain";
                    break;
                case "Enemy":
                    txtNewCode.Text = "ENEMY";
                    break;
                case "Lethal":
                    txtNewCode.Text = "DEAD";
                    break;
                case "EnemyBlocking":
                    txtNewCode.Text = "BLOCK";
                    break;
                case "Start":
                    txtNewCode.Text = "START";
                    break;
                case "Tree":
                    txtNewCode.Text = "TREE";
                    break;
                case "Location":
                    txtNewCode.Text = "LOCATION";
                    break;
                case "Clear":
                    txtNewCode.Text = "";
                    break;
                case "Custom":
                    txtNewCode.Text = "";
                    txtNewCode.Enabled = true;
                    break;
            }
        }

        private void listTiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listTiles.SelectedIndices.Count > 0)
            {
                EditorWindow.DrawTile = (int)listTiles.SelectedIndices[0];
                System.Diagnostics.Debug.WriteLine(listTiles.SelectedIndices[0]);
            }
        }

        private void RadioCode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPassable.Checked)
            {
                EditorWindow.EditingCode = false;
            }
            else
            {
                EditorWindow.EditingCode = true;
            }
        }

        private void txtNewCode_TextChanged(object sender, EventArgs e)
        {
           EditorWindow.CurrentCodeValue = txtNewCode.Text;
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorWindow.DrawLayer = 1;
            backgroundToolStripMenuItem.Checked = true;
            interactiveToolStripMenuItem.Checked = false;
            foregroundToolStripMenuItem.Checked = false;
        }

        private void interactiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorWindow.DrawLayer = 2;
            backgroundToolStripMenuItem.Checked = false;
            interactiveToolStripMenuItem.Checked = true;
            foregroundToolStripMenuItem.Checked = false;
        }

        private void foregroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorWindow.DrawLayer = 3;
            backgroundToolStripMenuItem.Checked = false;
            interactiveToolStripMenuItem.Checked = false;
            foregroundToolStripMenuItem.Checked = true;
        }

        private void loadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TileMap.LoadMap(new FileStream(Application.StartupPath + @"\MAP" + cboMapNumber
                    .Items[cboMapNumber.SelectedIndex] + ".MAP",FileMode.Open));
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.Print("Unable to Load Map File");
                
            }
        }

        private void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileMap.SaveMap(new FileStream(Application.StartupPath + @"\MAP" +
                cboMapNumber.Items[cboMapNumber.SelectedIndex] + ".MAP", FileMode.Create));
        }

        private void clearMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TileMap.ClearMap();
        }
    }
}

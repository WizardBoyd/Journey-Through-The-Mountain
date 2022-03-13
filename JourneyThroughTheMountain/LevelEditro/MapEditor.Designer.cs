
namespace LevelEditro
{
    partial class MapEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interactiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foregroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListTiles = new System.Windows.Forms.ImageList(this.components);
            this.listTiles = new System.Windows.Forms.ListView();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.groupBoxRightClick = new System.Windows.Forms.GroupBox();
            this.cboCodeValues = new System.Windows.Forms.ComboBox();
            this.lblCurrentCode = new System.Windows.Forms.Label();
            this.txtNewCode = new System.Windows.Forms.TextBox();
            this.RadioCode = new System.Windows.Forms.RadioButton();
            this.radioPassable = new System.Windows.Forms.RadioButton();
            this.lblMapNumber = new System.Windows.Forms.Label();
            this.cboMapNumber = new System.Windows.Forms.ComboBox();
            this.LstDebugBox = new System.Windows.Forms.ListBox();
            this.lstObjects = new System.Windows.Forms.ListView();
            this.loadObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditorWindow = new LevelEditro.EditorWindow();
            this.menuStrip1.SuspendLayout();
            this.groupBoxRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.layerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1069, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMapToolStripMenuItem,
            this.saveMapToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.loadMapToolStripMenuItem.Text = "Load Map";
            this.loadMapToolStripMenuItem.Click += new System.EventHandler(this.loadMapToolStripMenuItem_Click);
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.saveMapToolStripMenuItem.Text = "Save Map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.saveMapToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearMapToolStripMenuItem,
            this.loadObjectsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // clearMapToolStripMenuItem
            // 
            this.clearMapToolStripMenuItem.Name = "clearMapToolStripMenuItem";
            this.clearMapToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.clearMapToolStripMenuItem.Text = "Clear Map";
            this.clearMapToolStripMenuItem.Click += new System.EventHandler(this.clearMapToolStripMenuItem_Click);
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundToolStripMenuItem,
            this.interactiveToolStripMenuItem,
            this.foregroundToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // backgroundToolStripMenuItem
            // 
            this.backgroundToolStripMenuItem.Name = "backgroundToolStripMenuItem";
            this.backgroundToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.backgroundToolStripMenuItem.Text = "Background";
            this.backgroundToolStripMenuItem.Click += new System.EventHandler(this.backgroundToolStripMenuItem_Click);
            // 
            // interactiveToolStripMenuItem
            // 
            this.interactiveToolStripMenuItem.Name = "interactiveToolStripMenuItem";
            this.interactiveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.interactiveToolStripMenuItem.Text = "Interactive";
            this.interactiveToolStripMenuItem.Click += new System.EventHandler(this.interactiveToolStripMenuItem_Click);
            // 
            // foregroundToolStripMenuItem
            // 
            this.foregroundToolStripMenuItem.Name = "foregroundToolStripMenuItem";
            this.foregroundToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.foregroundToolStripMenuItem.Text = "Foreground";
            this.foregroundToolStripMenuItem.Click += new System.EventHandler(this.foregroundToolStripMenuItem_Click);
            // 
            // imgListTiles
            // 
            this.imgListTiles.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgListTiles.ImageSize = new System.Drawing.Size(32, 32);
            this.imgListTiles.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listTiles
            // 
            this.listTiles.HideSelection = false;
            this.listTiles.LargeImageList = this.imgListTiles;
            this.listTiles.Location = new System.Drawing.Point(12, 38);
            this.listTiles.MultiSelect = false;
            this.listTiles.Name = "listTiles";
            this.listTiles.Size = new System.Drawing.Size(121, 246);
            this.listTiles.TabIndex = 2;
            this.listTiles.TileSize = new System.Drawing.Size(32, 32);
            this.listTiles.UseCompatibleStateImageBehavior = false;
            this.listTiles.View = System.Windows.Forms.View.Tile;
            this.listTiles.SelectedIndexChanged += new System.EventHandler(this.listTiles_SelectedIndexChanged);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.LargeChange = 32;
            this.vScrollBar1.Location = new System.Drawing.Point(770, 41);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 493);
            this.vScrollBar1.TabIndex = 3;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.LargeChange = 32;
            this.hScrollBar1.Location = new System.Drawing.Point(149, 534);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(618, 17);
            this.hScrollBar1.TabIndex = 4;
            // 
            // groupBoxRightClick
            // 
            this.groupBoxRightClick.Controls.Add(this.cboCodeValues);
            this.groupBoxRightClick.Controls.Add(this.lblCurrentCode);
            this.groupBoxRightClick.Controls.Add(this.txtNewCode);
            this.groupBoxRightClick.Controls.Add(this.RadioCode);
            this.groupBoxRightClick.Controls.Add(this.radioPassable);
            this.groupBoxRightClick.Location = new System.Drawing.Point(12, 290);
            this.groupBoxRightClick.Name = "groupBoxRightClick";
            this.groupBoxRightClick.Size = new System.Drawing.Size(131, 120);
            this.groupBoxRightClick.TabIndex = 5;
            this.groupBoxRightClick.TabStop = false;
            this.groupBoxRightClick.Text = "Right Click Mode";
            // 
            // cboCodeValues
            // 
            this.cboCodeValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCodeValues.FormattingEnabled = true;
            this.cboCodeValues.Location = new System.Drawing.Point(6, 98);
            this.cboCodeValues.Name = "cboCodeValues";
            this.cboCodeValues.Size = new System.Drawing.Size(121, 21);
            this.cboCodeValues.TabIndex = 4;
            this.cboCodeValues.SelectedIndexChanged += new System.EventHandler(this.cboCodeValues_SelectedIndexChanged);
            // 
            // lblCurrentCode
            // 
            this.lblCurrentCode.AutoSize = true;
            this.lblCurrentCode.Location = new System.Drawing.Point(50, 82);
            this.lblCurrentCode.Name = "lblCurrentCode";
            this.lblCurrentCode.Size = new System.Drawing.Size(16, 13);
            this.lblCurrentCode.TabIndex = 3;
            this.lblCurrentCode.Text = ":--";
            // 
            // txtNewCode
            // 
            this.txtNewCode.Location = new System.Drawing.Point(63, 62);
            this.txtNewCode.Name = "txtNewCode";
            this.txtNewCode.Size = new System.Drawing.Size(62, 20);
            this.txtNewCode.TabIndex = 2;
            this.txtNewCode.TextChanged += new System.EventHandler(this.txtNewCode_TextChanged);
            // 
            // RadioCode
            // 
            this.RadioCode.AutoSize = true;
            this.RadioCode.Location = new System.Drawing.Point(6, 62);
            this.RadioCode.Name = "RadioCode";
            this.RadioCode.Size = new System.Drawing.Size(50, 17);
            this.RadioCode.TabIndex = 1;
            this.RadioCode.TabStop = true;
            this.RadioCode.Text = "Code";
            this.RadioCode.UseVisualStyleBackColor = true;
            this.RadioCode.CheckedChanged += new System.EventHandler(this.RadioCode_CheckedChanged);
            // 
            // radioPassable
            // 
            this.radioPassable.AutoSize = true;
            this.radioPassable.Checked = true;
            this.radioPassable.Location = new System.Drawing.Point(6, 39);
            this.radioPassable.Name = "radioPassable";
            this.radioPassable.Size = new System.Drawing.Size(104, 17);
            this.radioPassable.TabIndex = 0;
            this.radioPassable.TabStop = true;
            this.radioPassable.Text = "Toggle Passable";
            this.radioPassable.UseVisualStyleBackColor = true;
            this.radioPassable.CheckedChanged += new System.EventHandler(this.radioPassable_CheckedChanged);
            // 
            // lblMapNumber
            // 
            this.lblMapNumber.AutoSize = true;
            this.lblMapNumber.Location = new System.Drawing.Point(9, 425);
            this.lblMapNumber.Name = "lblMapNumber";
            this.lblMapNumber.Size = new System.Drawing.Size(68, 13);
            this.lblMapNumber.TabIndex = 6;
            this.lblMapNumber.Text = "Map Number";
            // 
            // cboMapNumber
            // 
            this.cboMapNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMapNumber.FormattingEnabled = true;
            this.cboMapNumber.Location = new System.Drawing.Point(87, 422);
            this.cboMapNumber.Name = "cboMapNumber";
            this.cboMapNumber.Size = new System.Drawing.Size(58, 21);
            this.cboMapNumber.TabIndex = 7;
            // 
            // LstDebugBox
            // 
            this.LstDebugBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LstDebugBox.FormattingEnabled = true;
            this.LstDebugBox.Location = new System.Drawing.Point(13, 456);
            this.LstDebugBox.Name = "LstDebugBox";
            this.LstDebugBox.Size = new System.Drawing.Size(120, 95);
            this.LstDebugBox.TabIndex = 8;
            // 
            // lstObjects
            // 
            this.lstObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstObjects.HideSelection = false;
            this.lstObjects.LargeImageList = this.imgListTiles;
            this.lstObjects.Location = new System.Drawing.Point(836, 41);
            this.lstObjects.MultiSelect = false;
            this.lstObjects.Name = "lstObjects";
            this.lstObjects.Size = new System.Drawing.Size(180, 510);
            this.lstObjects.TabIndex = 10;
            this.lstObjects.TileSize = new System.Drawing.Size(64, 64);
            this.lstObjects.UseCompatibleStateImageBehavior = false;
            this.lstObjects.View = System.Windows.Forms.View.Tile;
            this.lstObjects.SelectedIndexChanged += new System.EventHandler(this.lstObjects_SelectedIndexChanged);
            // 
            // loadObjectsToolStripMenuItem
            // 
            this.loadObjectsToolStripMenuItem.Name = "loadObjectsToolStripMenuItem";
            this.loadObjectsToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.loadObjectsToolStripMenuItem.Text = "Load Objects";
            this.loadObjectsToolStripMenuItem.Click += new System.EventHandler(this.loadObjectsToolStripMenuItem_Click);
            // 
            // EditorWindow
            // 
            this.EditorWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EditorWindow.Location = new System.Drawing.Point(149, 38);
            this.EditorWindow.MouseHoverUpdatesOnly = false;
            this.EditorWindow.Name = "EditorWindow";
            this.EditorWindow.Size = new System.Drawing.Size(618, 493);
            this.EditorWindow.TabIndex = 9;
            this.EditorWindow.Text = "EditorWindow";
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 571);
            this.Controls.Add(this.lstObjects);
            this.Controls.Add(this.EditorWindow);
            this.Controls.Add(this.LstDebugBox);
            this.Controls.Add(this.cboMapNumber);
            this.Controls.Add(this.lblMapNumber);
            this.Controls.Add(this.groupBoxRightClick);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.listTiles);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MapEditor";
            this.Text = "MapEditor";
            this.Load += new System.EventHandler(this.MapEditor_Load);
            this.Resize += new System.EventHandler(this.MapEditor_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxRightClick.ResumeLayout(false);
            this.groupBoxRightClick.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interactiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem foregroundToolStripMenuItem;
        private System.Windows.Forms.ImageList imgListTiles;
        private System.Windows.Forms.ListView listTiles;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.GroupBox groupBoxRightClick;
        private System.Windows.Forms.TextBox txtNewCode;
        private System.Windows.Forms.RadioButton RadioCode;
        private System.Windows.Forms.RadioButton radioPassable;
        private System.Windows.Forms.Label lblCurrentCode;
        private System.Windows.Forms.ComboBox cboCodeValues;
        private System.Windows.Forms.Label lblMapNumber;
        private System.Windows.Forms.ComboBox cboMapNumber;
        private System.Windows.Forms.ListBox LstDebugBox;
        public EditorWindow EditorWindow;
        private System.Windows.Forms.ListView lstObjects;
        private System.Windows.Forms.ToolStripMenuItem loadObjectsToolStripMenuItem;
    }
}
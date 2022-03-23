
namespace DialougeEditor
{
    partial class DialougeEditor
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
            this.saveDialougeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDialougeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllDialougeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertyList = new System.Windows.Forms.PropertyGrid();
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createNewDialougeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBasicNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDialougeNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DialougeEditorWindow = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveDialougeToolStripMenuItem,
            this.loadDialougeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveDialougeToolStripMenuItem
            // 
            this.saveDialougeToolStripMenuItem.Name = "saveDialougeToolStripMenuItem";
            this.saveDialougeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.saveDialougeToolStripMenuItem.Text = "Save Dialouge";
            // 
            // loadDialougeToolStripMenuItem
            // 
            this.loadDialougeToolStripMenuItem.Name = "loadDialougeToolStripMenuItem";
            this.loadDialougeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.loadDialougeToolStripMenuItem.Text = "Load Dialouge";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllDialougeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // clearAllDialougeToolStripMenuItem
            // 
            this.clearAllDialougeToolStripMenuItem.Name = "clearAllDialougeToolStripMenuItem";
            this.clearAllDialougeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.clearAllDialougeToolStripMenuItem.Text = "Clear All Dialouge";
            // 
            // PropertyList
            // 
            this.PropertyList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PropertyList.Location = new System.Drawing.Point(0, 27);
            this.PropertyList.Name = "PropertyList";
            this.PropertyList.Size = new System.Drawing.Size(200, 411);
            this.PropertyList.TabIndex = 1;
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewDialougeToolStripMenuItem,
            this.deleteDialougeNodeToolStripMenuItem});
            this.ContextMenu.Name = "ContextMenu";
            this.ContextMenu.Size = new System.Drawing.Size(191, 48);
            this.ContextMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ContextMenu_MouseClick);
            this.ContextMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContextMenu_MouseDown);
            // 
            // createNewDialougeToolStripMenuItem
            // 
            this.createNewDialougeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createBasicNodeToolStripMenuItem});
            this.createNewDialougeToolStripMenuItem.Name = "createNewDialougeToolStripMenuItem";
            this.createNewDialougeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.createNewDialougeToolStripMenuItem.Text = "Create Dialouge Node";
            this.createNewDialougeToolStripMenuItem.Click += new System.EventHandler(this.createNewDialougeToolStripMenuItem_Click);
            // 
            // createBasicNodeToolStripMenuItem
            // 
            this.createBasicNodeToolStripMenuItem.Name = "createBasicNodeToolStripMenuItem";
            this.createBasicNodeToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.createBasicNodeToolStripMenuItem.Text = "Create Basic Node";
            this.createBasicNodeToolStripMenuItem.Click += new System.EventHandler(this.createBasicNodeToolStripMenuItem_Click);
            // 
            // deleteDialougeNodeToolStripMenuItem
            // 
            this.deleteDialougeNodeToolStripMenuItem.Name = "deleteDialougeNodeToolStripMenuItem";
            this.deleteDialougeNodeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.deleteDialougeNodeToolStripMenuItem.Text = "Delete Dialouge Node";
            // 
            // DialougeEditorWindow
            // 
            this.DialougeEditorWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DialougeEditorWindow.ContextMenuStrip = this.ContextMenu;
            this.DialougeEditorWindow.Location = new System.Drawing.Point(206, 27);
            this.DialougeEditorWindow.Name = "DialougeEditorWindow";
            this.DialougeEditorWindow.Size = new System.Drawing.Size(582, 411);
            this.DialougeEditorWindow.TabIndex = 3;
            // 
            // DialougeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DialougeEditorWindow);
            this.Controls.Add(this.PropertyList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DialougeEditor";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.DialougeEditor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDialougeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDialougeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllDialougeToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid PropertyList;
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem createNewDialougeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDialougeNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createBasicNodeToolStripMenuItem;
        private System.Windows.Forms.Panel DialougeEditorWindow;
    }
}


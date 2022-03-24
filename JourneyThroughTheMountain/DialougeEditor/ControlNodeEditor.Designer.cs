
namespace DialougeEditor
{
    partial class ControlNodeEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nodeEditor1 = new DialougeEditor.NodeEditor();
            this.SuspendLayout();
            // 
            // nodeEditor1
            // 
            this.nodeEditor1.Context = null;
            this.nodeEditor1.Location = new System.Drawing.Point(0, 0);
            this.nodeEditor1.Name = "nodeEditor1";
            this.nodeEditor1.Size = new System.Drawing.Size(150, 150);
            this.nodeEditor1.TabIndex = 0;
            // 
            // ControlNodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nodeEditor1);
            this.Name = "ControlNodeEditor";
            this.ResumeLayout(false);

        }


        #endregion

        public NodeEditor nodeEditor1;
    }
}

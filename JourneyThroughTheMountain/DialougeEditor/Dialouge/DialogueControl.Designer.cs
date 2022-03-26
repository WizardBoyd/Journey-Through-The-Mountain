
namespace DialougeEditor
{
    partial class DialogueControl
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
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.PlusButton = new System.Windows.Forms.Button();
            this.MinusBox = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(200, 279);
            this.flowLayoutPanel.TabIndex = 1;
            // 
            // PlusButton
            // 
            this.PlusButton.Location = new System.Drawing.Point(245, 91);
            this.PlusButton.Name = "PlusButton";
            this.PlusButton.Size = new System.Drawing.Size(75, 45);
            this.PlusButton.TabIndex = 2;
            this.PlusButton.Text = "+";
            this.PlusButton.UseVisualStyleBackColor = true;
            // 
            // MinusBox
            // 
            this.MinusBox.Location = new System.Drawing.Point(245, 142);
            this.MinusBox.Name = "MinusBox";
            this.MinusBox.Size = new System.Drawing.Size(75, 45);
            this.MinusBox.TabIndex = 3;
            this.MinusBox.Text = "-";
            this.MinusBox.UseVisualStyleBackColor = true;
            // 
            // DialogueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MinusBox);
            this.Controls.Add(this.PlusButton);
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "DialogueControl";
            this.Size = new System.Drawing.Size(353, 285);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        protected System.Windows.Forms.Button PlusButton;
        protected System.Windows.Forms.Button MinusBox;
    }
}

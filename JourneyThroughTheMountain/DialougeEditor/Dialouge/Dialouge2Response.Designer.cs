
namespace DialougeEditor.Dialouge
{
    partial class Dialouge2Response
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
            this.TextBox1 = new System.Windows.Forms.RichTextBox();
            this.TextBox2 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(3, 14);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(261, 96);
            this.TextBox1.TabIndex = 0;
            this.TextBox1.Text = "";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(3, 138);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(261, 96);
            this.TextBox2.TabIndex = 1;
            this.TextBox2.Text = "";
            // 
            // Dialouge2Response
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.TextBox1);
            this.Name = "Dialouge2Response";
            this.Size = new System.Drawing.Size(267, 261);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.RichTextBox TextBox1;
        protected System.Windows.Forms.RichTextBox TextBox2;
    }
}

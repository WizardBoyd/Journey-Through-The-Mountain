
namespace DialougeEditor.Dialouge
{
    partial class SpeakControl
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
            this.TextBox = new System.Windows.Forms.RichTextBox();
            this.AIspeaking = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // TextBox
            // 
            this.TextBox.Location = new System.Drawing.Point(3, 3);
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(147, 203);
            this.TextBox.TabIndex = 0;
            this.TextBox.Text = "";
            // 
            // AIspeaking
            // 
            this.AIspeaking.AutoSize = true;
            this.AIspeaking.Location = new System.Drawing.Point(156, 88);
            this.AIspeaking.Name = "AIspeaking";
            this.AIspeaking.Size = new System.Drawing.Size(80, 17);
            this.AIspeaking.TabIndex = 1;
            this.AIspeaking.TabStop = true;
            this.AIspeaking.Text = "AISpeaking";
            this.AIspeaking.UseVisualStyleBackColor = true;
            // 
            // SpeakControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AIspeaking);
            this.Controls.Add(this.TextBox);
            this.Name = "SpeakControl";
            this.Size = new System.Drawing.Size(255, 209);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.RadioButton AIspeaking;
        protected System.Windows.Forms.RichTextBox TextBox;
    }
}

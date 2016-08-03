namespace MyUtil.Extensions
{
    partial class MessengerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessengerForm));
            this._messageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // _messageRichTextBox
            // 
            this._messageRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._messageRichTextBox.Location = new System.Drawing.Point(0, 0);
            this._messageRichTextBox.Name = "_messageRichTextBox";
            this._messageRichTextBox.ReadOnly = true;
            this._messageRichTextBox.Size = new System.Drawing.Size(809, 628);
            this._messageRichTextBox.TabIndex = 1;
            this._messageRichTextBox.Text = "";
            // 
            // MessengerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 628);
            this.Controls.Add(this._messageRichTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessengerForm";
            this.Text = "MessengerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox _messageRichTextBox;
    }
}
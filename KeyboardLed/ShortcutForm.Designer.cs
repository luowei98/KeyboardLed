namespace KeyboardLed
{
    partial class ShortcutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortcutForm));
            this.SuspendLayout();
            // 
            // ShortcutForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ShortcutForm";
            this.Opacity = 0.8D;
            this.ShowInTaskbar = false;
            this.Text = "w";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Deactivate += new System.EventHandler(this.ShortcutForm_Deactivate);
            this.Load += new System.EventHandler(this.ShortcutForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ShortcutForm_KeyUp);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ShortcutForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
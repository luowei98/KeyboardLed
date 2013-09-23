namespace KeyboardLed
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictCharacter = new System.Windows.Forms.PictureBox();
            this.picNumber = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictCharacter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // pictCharacter
            // 
            this.pictCharacter.Image = ((System.Drawing.Image)(resources.GetObject("pictCharacter.Image")));
            this.pictCharacter.Location = new System.Drawing.Point(0, 80);
            this.pictCharacter.Name = "pictCharacter";
            this.pictCharacter.Size = new System.Drawing.Size(64, 64);
            this.pictCharacter.TabIndex = 4;
            this.pictCharacter.TabStop = false;
            // 
            // picNumber
            // 
            this.picNumber.Image = global::KeyboardLed.Properties.Resources.NotNumber;
            this.picNumber.Location = new System.Drawing.Point(0, 0);
            this.picNumber.Name = "picNumber";
            this.picNumber.Size = new System.Drawing.Size(64, 64);
            this.picNumber.TabIndex = 3;
            this.picNumber.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(64, 144);
            this.Controls.Add(this.pictCharacter);
            this.Controls.Add(this.picNumber);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Opacity = 0.8D;
            this.ShowInTaskbar = false;
            this.Text = "MainForm";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictCharacter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picNumber;
        private System.Windows.Forms.PictureBox pictCharacter;
    }
}
namespace KeyboardLed
{
    partial class SpeakerForm
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
            this.FadeoutTimer = new System.Windows.Forms.Timer(this.components);
            this.CloseTimer = new System.Windows.Forms.Timer(this.components);
            this.picSpeaker = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSpeaker)).BeginInit();
            this.SuspendLayout();
            // 
            // FadeoutTimer
            // 
            this.FadeoutTimer.Interval = 10;
            this.FadeoutTimer.Tick += new System.EventHandler(this.FadeoutTimer_Tick);
            // 
            // CloseTimer
            // 
            this.CloseTimer.Interval = 800;
            this.CloseTimer.Tick += new System.EventHandler(this.CloseTimer_Tick);
            // 
            // picSpeaker
            // 
            this.picSpeaker.Image = global::KeyboardLed.Properties.Resources.SpeakerOff;
            this.picSpeaker.Location = new System.Drawing.Point(0, 0);
            this.picSpeaker.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.picSpeaker.Name = "picSpeaker";
            this.picSpeaker.Size = new System.Drawing.Size(64, 64);
            this.picSpeaker.TabIndex = 2;
            this.picSpeaker.TabStop = false;
            // 
            // SpeakerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(64, 64);
            this.Controls.Add(this.picSpeaker);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SpeakerForm";
            this.ShowInTaskbar = false;
            this.Text = "MainForm";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.SpeakerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSpeaker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picSpeaker;
        private System.Windows.Forms.Timer FadeoutTimer;
        private System.Windows.Forms.Timer CloseTimer;
    }
}
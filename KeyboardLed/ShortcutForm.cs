#region file header

// -----------------------------------------------------------------------------
// Project: KeyboardLed.KeyboardLed
// File:    SpeakerForm.cs
// Author:  Robert.L
// Created: 2013/09/23 17:13
// -----------------------------------------------------------------------------

#endregion

namespace KeyboardLed
{
    #region using statements

    using System;
    using System.Drawing;
    using System.Windows.Forms;

    #endregion

    /// <summary>The shortcut form.</summary>
    public partial class ShortcutForm : TransForm
    {
        /// <summary>The location.</summary>
        private Point location;

        /// <summary>The curr opacity.</summary>
        private double currOpacity;

        /// <summary>Initializes a new instance of the <see cref="ShortcutForm"/> class.</summary>
        public ShortcutForm()
        {
            InitializeComponent();
        }

        /// <summary>The set location.</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void SetLocation(int x, int y)
        {
            location = new Point(x, y);
        }

        /// <summary>The show.</summary>
        /// <param name="mute">The mute.</param>
        public void Show(bool mute)
        {
            this.Init();

            //this.picSpeaker.Image = mute ? Properties.Resources.SpeakerOff : Properties.Resources.SpeakerLouder;
            AudioHelp.SetMute(mute);

            Application.DoEvents();

            this.Show();
            this.Location = location;

            //CloseTimer.Enabled = true;
        }

        /// <summary>The speaker form_ load.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void SpeakerForm_Load(object sender, EventArgs e)
        {
            currOpacity = this.Opacity;
        }

        /// <summary>The close timer_ tick.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            this.Enabled = false;
            //FadeoutTimer.Enabled = true;
        }

        /// <summary>The fadeout timer_ tick.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void FadeoutTimer_Tick(object sender, EventArgs e)
        {
            if (currOpacity > 0)
            {
                currOpacity -= 0.01;
                this.Opacity = currOpacity;
            }
            else
            {
                this.Init();
            }
        }

        /// <summary>The init.</summary>
        private void Init()
        {
            //CloseTimer.Enabled = false;
            //FadeoutTimer.Enabled = false;
            this.Hide();
            this.Opacity = 0.8;
            this.currOpacity = 0.8;
        }
    }
}
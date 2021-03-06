﻿#region file header

// -----------------------------------------------------------------------------
// Project: KeyboardLed.KeyboardLed
// File:    MainForm.cs
// Author:  Robert.L
// Created: 2013/09/23 14:55
// -----------------------------------------------------------------------------

#endregion

namespace KeyboardLed
{
    #region using statements

    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using KeyboardLed.Properties;

    #endregion

    /// <summary>The main form.</summary>
    public partial class MainForm : TransForm
    {
        /// <summary>The speaker.</summary>
        private static readonly SpeakerForm speaker = new SpeakerForm();

        private static readonly ShortcutForm shortcut = new ShortcutForm();

        /// <summary>The hook.</summary>
        private static readonly KeyboardHook hook = new KeyboardHook();

        /// <summary>The speaker mute.</summary>
        private static bool speakerMute;

        /// <summary>The numlock visible.</summary>
        private static bool numlockVisible;

        /// <summary>The capslock visible.</summary>
        private static bool capslockVisible;

        private long controlDownTime = 0;

        /// <summary>Initializes a new instance of the <see cref="MainForm"/> class.</summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>The main form_ load.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // for unknow reason width be changed to 136, forced to 64
            this.ClientSize = new Size(64, 144);

            speakerMute = AudioHelp.IsMute();
            numlockVisible = !IsKeyLocked(Keys.NumLock);
            capslockVisible = IsKeyLocked(Keys.CapsLock);
            UpdateVisiable();

            hook.HookedKeys.Add(Keys.CapsLock);
            hook.HookedKeys.Add(Keys.NumLock);
            hook.HookedKeys.Add(Keys.Pause);
            hook.HookedKeys.Add(Keys.LControlKey);

            hook.KeyDown += this.Global_KeyDown;
            hook.KeyUp += this.Global_KeyUp;

            SetPosition();
        }

        /// <summary>The global_ key down.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Global_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.NumLock:
                    {
                        numlockVisible = IsKeyLocked(Keys.NumLock);
                        UpdateVisiable();
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.ExclamationErrMsg01, Resources.ExclamationErrTitle, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }

            e.Handled = false;
        }

        /// <summary>The global_ key up.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Global_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Pause:
                    {
                        AudioHelp.SetMute(speakerMute);
                        speakerMute = !AudioHelp.IsMute();
                        speaker.Show(speakerMute);
                        break;
                    }

                    case Keys.CapsLock:
                    {
                        capslockVisible = IsKeyLocked(Keys.CapsLock);
                        UpdateVisiable();
                        break;
                    }

                    case Keys.LControlKey:
                    {
                        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        if (now - controlDownTime < 500)
                        {
                            shortcut.Show();
                            shortcut.Activate();
                            controlDownTime = 0;
                        }
                        else
                        {
                            controlDownTime = now;
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.ExclamationErrMsg01, Resources.ExclamationErrTitle, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }

            e.Handled = false;
        }

        /// <summary>The update visiable.</summary>
        private void UpdateVisiable()
        {
            picNumber.Visible = numlockVisible;
            pictCharacter.Visible = capslockVisible;

            this.Visible = capslockVisible || numlockVisible;
            this.Refresh();
        }

        /// <summary>The set position.</summary>
        private void SetPosition()
        {
            var x = Screen.PrimaryScreen.Bounds.Right - this.ClientSize.Width - 50;
            var y = Screen.PrimaryScreen.Bounds.Bottom - this.ClientSize.Height - 50;

            this.Location = new Point(x, y);

            speaker.SetLocation(this.Location.X, this.Location.Y - speaker.Height - 16);
        }
    }
}
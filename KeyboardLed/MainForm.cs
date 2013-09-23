using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardLed
{
    public partial class MainForm : TransForm
    {
        private static SpeakerForm speaker = new SpeakerForm();

        private static KeyboardHook hook = new KeyboardHook();

        private static bool speakerMute;

        private static bool numlockVisible;

        private static bool capslockVisible;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            speakerMute = !AudioHelp.IsMute();
            numlockVisible = !IsKeyLocked(Keys.NumLock);
            capslockVisible = IsKeyLocked(Keys.CapsLock);
            UpdateVisiable();

            SetPosition();

            hook.HookedKeys.Add(Keys.CapsLock);
            hook.HookedKeys.Add(Keys.NumLock);
            hook.HookedKeys.Add(Keys.Scroll);

            hook.KeyDown += this.Global_KeyDown;
        }

        private void Global_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Scroll:
                    {
                        speakerMute = !AudioHelp.IsMute();
                        speaker.Show(speakerMute);
                        break;
                    }
                case Keys.NumLock:
                    {
                        numlockVisible = !numlockVisible;
                        UpdateVisiable();
                        break;
                    }
                case Keys.CapsLock:
                    {
                        capslockVisible = !capslockVisible;
                        UpdateVisiable();
                        break;
                    }
            }

            e.Handled = false;
        }

        private void UpdateVisiable()
        {
            picNumber.Visible = numlockVisible;
            pictCharacter.Visible = capslockVisible;

            this.Visible = capslockVisible || numlockVisible;
            this.Refresh();
        }

        private void SetPosition()
        {
            var x = Screen.PrimaryScreen.Bounds.Right - this.Width + 50;
            var y = Screen.PrimaryScreen.Bounds.Bottom - this.Height - 50;

            this.Location = new Point(x, y);

            speaker.SetLocation(this.Location.X, this.Location.Y - speaker.Height - 16);
        }

    }
}

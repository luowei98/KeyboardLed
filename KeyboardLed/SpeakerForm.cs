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
    using CoreAudioApi;

    public partial class SpeakerForm : TransForm
    {
        private Point location;

        private double currOpacity;


        public SpeakerForm()
        {
            InitializeComponent();
        }

        public void SetLocation(int x, int y)
        {
            location = new Point(x, y);
        }

        public void Show(bool mute)
        {
            this.Init();

            this.picSpeaker.Image = mute ? Properties.Resources.SpeakerOff : Properties.Resources.SpeakerLouder;
            AudioHelp.SetMute(mute);

            Application.DoEvents();

            this.Show();
            this.Location = location;

            CloseTimer.Enabled = true;
        }

        private void SpeakerForm_Load(object sender, EventArgs e)
        {
            currOpacity = this.Opacity;
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            this.Enabled = false;
            FadeoutTimer.Enabled = true;
        }

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

        private void Init()
        {
            CloseTimer.Enabled = false;
            FadeoutTimer.Enabled = false;
            this.Hide();
            this.Opacity = 0.8;
            this.currOpacity = 0.8;
        }
    }
}

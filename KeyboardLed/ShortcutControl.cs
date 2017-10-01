using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeyboardLed
{
    public partial class ShortcutControl : UserControl
    {
        public static int DefaultWidth = 128;

        public new int Width
        {
            get { return this.Width; }
            set
            {
                pictureBox.Size = new Size(value, value);
                label.Width = value;
                base.Width = value;
            }
        }
        public string Path { get; private set; }
        public string Caption { get; private set; }

        public ShortcutControl()
        {
            InitializeComponent();
            this.Width = DefaultWidth;
        }
        public void Init(string path, string caption, int iconIdx = 0)
        {
            this.Path = path;
            this.Caption = caption;

            pictureBox.BackgroundImage = getImage(path, iconIdx);
            label.Text = caption;
        }

        private Image getImage(string path, int index)
        {
            var icon = IconHelp.GetHighestIcon(path, index);
            return icon != null ? icon.ToBitmap() : null;
        }

        public event EventHandler IconClick
        {
            add
            {
                pictureBox.Click += value;
                label.Click += value;
            }
            remove
            {
                pictureBox.Click -= value;
                label.Click -= value;
            }
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            label.ForeColor = Color.White;
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            label.ForeColor = Color.Gainsboro;
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            label.ForeColor = Color.White;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            label.ForeColor = Color.Gainsboro;
        }
    }
}

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KeyboardLed
{
    public partial class ShortcutControl : UserControl
    {
        public static int DefaultWidth = 128;

        private readonly string[] haveIconFile = {".dll", ".exe"};

        public new int Width
        {
            // ReSharper disable once FunctionRecursiveOnAllPaths
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


        public void Init(string path)
        {
            this.Path = path;

            Image img;
            if (IsDirectory(path))
            {
                img = IconHelp.GetFolderIcon(path)?.ToBitmap();
            }
            else
            {
                var ext = System.IO.Path.GetExtension(path);
                if (ext != null && haveIconFile.Contains(ext.ToLower()))
                {
                    img = IconHelp.GetHighestIcon(path)?.ToBitmap();
                }
                else
                {
                    img = IconHelp.GetHighestExtensionIcon(path)?.ToBitmap();
                }
                if (img == null)
                {
                    img = IconHelp.GetDefaultIcon()?.ToBitmap();
                }
            }

            pictureBox.BackgroundImage = img;
            label.Text = InitCaption(path);
        }

        public static bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            return attr.HasFlag(FileAttributes.Directory);
        }

        private string InitCaption(string path)
        {
            this.Caption = IsDirectory(path) ? path : System.IO.Path.GetFileNameWithoutExtension(path);

            return this.Caption;
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

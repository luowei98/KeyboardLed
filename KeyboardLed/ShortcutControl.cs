using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KeyboardLed
{
    public partial class ShortcutControl : UserControl
    {
        public enum PathTypeEnum
        {
            Folder,
            Exe,
            Other
        }

        public static int DefaultWidth = 128;
        public PathTypeEnum PathType = PathTypeEnum.Other;

        private readonly string[] haveIconFile = {".dll", ".exe"};

        public new int Width
        {
            get { return base.Width; }
            set
            {
                pictureBox.Size = new Size(value, value);
                label.Width = value;
                base.Width = value;
            }
        }

        public string Path { get; private set; }


        public ShortcutControl()
        {
            InitializeComponent();
            this.Width = DefaultWidth;
        }


        public void Init(string path)
        {
            this.Path = path;
            setPathType();
            setCaption();

            Image img;
            switch (this.PathType)
            {
                case PathTypeEnum.Folder:
                    img = IconHelp.GetFolderIcon(path)?.ToBitmap();
                    break;
                case PathTypeEnum.Exe:
                    img = IconHelp.GetHighestIcon(path)?.ToBitmap();
                    break;
                case PathTypeEnum.Other:
                    img = IconHelp.GetHighestExtensionIcon(path)?.ToBitmap();
                    break;
                default:
                    img = null;
                    break;
            }
            if (img == null)
            {
                img = IconHelp.GetDefaultIcon()?.ToBitmap();
            }

            pictureBox.BackgroundImage = img;
        }

        private void setPathType()
        {
            if (IsDirectory(this.Path))
            {
                this.PathType = PathTypeEnum.Folder;
            }
            else
            {
                var ext = System.IO.Path.GetExtension(this.Path);
                if (ext != null && haveIconFile.Contains(ext.ToLower()))
                {
                    this.PathType = PathTypeEnum.Exe;
                }
                else
                {
                    this.PathType = PathTypeEnum.Other;
                }
            }
        }

        public static bool IsDirectory(string path)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(path);
                return attr.HasFlag(FileAttributes.Directory);
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        private void setCaption()
        {
            var caption = "";
            switch (this.PathType)
            {
                case PathTypeEnum.Folder:
                    caption = this.Path;
                    break;
                case PathTypeEnum.Exe:
                    caption = System.IO.Path.GetFileNameWithoutExtension(this.Path);
                    break;
                case PathTypeEnum.Other:
                    caption = System.IO.Path.GetFileName(this.Path);
                    break;
            }

            label.Text = truncateText(caption);
        }

        private string truncateText(string text)
        {
            var g = this.CreateGraphics();
            var size = g.MeasureString(text, label.Font);
            if (size.Width <= label.Width)
            {
                return text;
            }

            var startText = text.Substring(0, 3) + "...";
            var tailText = "";
            var tailStart = 0;
            while (g.MeasureString(startText + tailText, label.Font).Width < label.Width)
            {
                tailStart++;
                tailText = text.Substring(text.Length - tailStart);
            }

            return startText + text.Substring(text.Length - tailStart + 1);
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

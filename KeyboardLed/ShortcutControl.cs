﻿using System;
using System.Diagnostics;
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
        public string Caption { get; private set; }


        public ShortcutControl()
        {
            InitializeComponent();
            this.Width = DefaultWidth;
        }


        public void Init(string path)
        {
            var p = path.Split("|".ToCharArray());

            this.Path = p[0];
            var caption = "";
            if (p.Length > 1)
            {
                caption = p[1];
            }
            setPathType();
            setCaption(caption);

            Image img;
            switch (this.PathType)
            {
                case PathTypeEnum.Folder:
                    img = IconHelp.GetFolderIcon(this.Path)?.ToBitmap();
                    break;
                case PathTypeEnum.Exe:
                    img = IconHelp.GetHighestIcon(this.Path)?.ToBitmap();
                    break;
                case PathTypeEnum.Other:
                    img = IconHelp.GetHighestExtensionIcon(this.Path)?.ToBitmap();
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

            toolTip.SetToolTip(pictureBox, this.Path);
            toolTip.SetToolTip(label, this.Path);
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

        private void setCaption(string caption)
        {
            if (caption.Length == 0)
            {
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
            }

            this.Caption = truncateText(caption);
            label.Text = this.Caption;
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

        public void Run()
        {
            if (string.IsNullOrEmpty(this.Path))
            {
                return;
            }
            var dir = System.IO.Path.GetDirectoryName(this.Path);

            if (this.PathType == PathTypeEnum.Exe)
            {
                var psi = new ProcessStartInfo(this.Path)
                {
                    UseShellExecute = false,
                };
                if (!string.IsNullOrEmpty(dir))
                {
                    psi.WorkingDirectory = dir;
                }
                Process.Start(psi);
            }
            else
            {
                Process.Start(this.Path);
            }
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

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Parent.Hide();
                Run();
            }
            if (e.Button == MouseButtons.Right)
            {
                var ctxMenu = new ShellContextMenu();
                FileInfo[] fileInfos = {new FileInfo(this.Path)};

                ctxMenu.ShowContextMenu(fileInfos, this.PointToScreen(new Point(e.X, e.Y)));
            }
        }
    }
}

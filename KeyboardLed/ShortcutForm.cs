#region file header

// -----------------------------------------------------------------------------
// Project: KeyboardLed.KeyboardLed
// File:    SpeakerForm.cs
// Author:  Robert.L
// Created: 2013/09/23 17:13
// -----------------------------------------------------------------------------

#endregion

using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace KeyboardLed
{
    #region using statements

    using System;
    using System.Drawing;
    using System.Windows.Forms;

    #endregion

    /// <summary>The shortcut form.</summary>
    public partial class ShortcutForm : Form
    {
        private const int col = 8;
        private const int row = 4;
        private const int xMargin = 100;
        private const int yMargin = 60;

        public sealed override Size MaximumSize
        {
            get { return base.MaximumSize; }
            set { base.MaximumSize = value; }
        }

        public List<string> items { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ShortcutForm"/> class.</summary>
        public ShortcutForm()
        {
            InitializeComponent();

            // cover screen
            var area = Screen.FromControl(this).WorkingArea;
            this.Width = area.Size.Width;
            this.Height = area.Size.Height;
            this.Location = area.Location;

            items = new List<string>();
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Program Files (x86)\Microsoft VS Code\Code.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"D:\Documents\Visual Studio 2015\Projects\KeyboardLed");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");
            items.Add(@"C:\Windows\explorer.exe");

            // init shortcut
            var xy = new Point(0, 0);
            var xOffset = (this.Width - xMargin * 2) / col;
            var yOffset = (this.Height - yMargin * 2) / row;
            var xStart = (xOffset - ShortcutControl.DefaultWidth) / 2;
            var yStart = (yOffset - ShortcutControl.DefaultWidth) / 2;
            foreach (var i in items)
            {
                var shortcut = new ShortcutControl()
                {
                    Location = new Point(xMargin + xStart + xOffset * xy.X, yMargin + yStart + yOffset * xy.Y),
                    TabStop = true,
                };
                if (IsDirectory(i))
                {
                    shortcut.Init(@"C:\WINDOWS\system32\imageres.dll", GetCaption(i, true), 5);
                }
                else
                {
                    shortcut.Init(i, GetCaption(i, false));
                }
                shortcut.IconClick += this.pictureBox1_Click;
                this.Controls.Add(shortcut);

                if (++xy.X < col) continue;
                xy.X = xy.X % col;
                xy.Y++;
            }
        }

        private void ShortcutForm_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ShortcutForm_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.TopMost = true;
        }

        private bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            return attr.HasFlag(FileAttributes.Directory);
        }

        private string GetCaption(string path, bool isDir)
        {
            return isDir ? path : Path.GetFileNameWithoutExtension(path);
        }

        private Bitmap getIcon(string path)
        {
            //var fi = File.GetAttributes(path);
            //var o = Icon.ExtractAssociatedIcon((fi & FileAttributes.Directory) == FileAttributes.Directory ? @"C:\WINDOWS\system32\imageres.dll" : path);
            var o = IconHelp.GetHighestIcon(@"C:\WINDOWS\system32\imageres.dll", 1);

            return o?.ToBitmap();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }

        private void ShortcutForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }

        private void ShortcutForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Hide();
            }
        }
        
    }
}
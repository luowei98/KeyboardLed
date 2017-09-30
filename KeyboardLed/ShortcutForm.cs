#region file header

// -----------------------------------------------------------------------------
// Project: KeyboardLed.KeyboardLed
// File:    SpeakerForm.cs
// Author:  Robert.L
// Created: 2013/09/23 17:13
// -----------------------------------------------------------------------------

#endregion

using System.Collections.Generic;
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
        private const int shortcutIconSize = 128;
        private const int col = 8;
        private const int row = 4;

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
            items.Add(@"D:\Documents\Visual Studio 2015\Projects\KeyboardLed");

            // init shortcut
            var left = (this.Width / col - shortcutIconSize) / 2;
            var top = (this.Height / row - shortcutIconSize) / 2;
            foreach (var i in items)
            {
                var pic = new PictureBox
                {
                    Location = new Point(left, top),
                    Size = new Size(128, 128),
                    TabStop = true,
                    BackgroundImage = getIcon(i),
                    BackgroundImageLayout = ImageLayout.Stretch,
                    BackColor = Color.Black,
                };
                pic.Click += this.pictureBox1_Click;
                this.Controls.Add(pic);
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

        private Bitmap getIcon(string path)
        {
            //var fi = File.GetAttributes(path);
            //var o = Icon.ExtractAssociatedIcon((fi & FileAttributes.Directory) == FileAttributes.Directory ? @"C:\WINDOWS\system32\imageres.dll" : path);
            var o = ShellIcon.GetLargeFolderIcon();

            return o?.ToBitmap();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
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
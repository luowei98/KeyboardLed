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
        private const string shortcutFileName = "shortcuts.txt";

        private readonly List<ShortcutControl> shortcutList = new List<ShortcutControl>();

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

            loadItems();
        }


        private void loadItems()
        {
            this.UseWaitCursor = true;

            try
            {
                // clear items
                foreach (var ctrl in shortcutList)
                {
                    this.Controls.Remove(ctrl);
                }
                shortcutList.Clear();

                items = getItems();

                // init shortcut
                var xy = new Point(0, 0);
                var xOffset = (this.Width - xMargin*2)/col;
                var yOffset = (this.Height - yMargin*2)/row;
                var xStart = (xOffset - ShortcutControl.DefaultWidth)/2;
                var yStart = (yOffset - ShortcutControl.DefaultWidth)/2;
                foreach (var i in items)
                {
                    var shortcut = new ShortcutControl()
                    {
                        Location = new Point(xMargin + xStart + xOffset*xy.X, yMargin + yStart + yOffset*xy.Y),
                        TabStop = true,
                    };

                    shortcut.Init(i);
                    this.Controls.Add(shortcut);

                    shortcutList.Add(shortcut);

                    if (++xy.X < col) continue;
                    xy.X = xy.X%col;
                    xy.Y++;
                }
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }

        private List<string> getItems()
        {
            var dir = Path.GetDirectoryName(Application.ExecutablePath);
            if (dir == null)
            {
                return new List<string>();
            }

            var path = Path.Combine(dir, shortcutFileName);
            if (!File.Exists(path))
            {
                return new List<string>();
            }

            var lines = File.ReadAllLines(path);
            var shortcuts = new List<string>();
            shortcuts.AddRange(lines);

            return shortcuts;
        }


        private void ShortcutForm_Load(object sender, EventArgs e)
        {
            textBox.Hide();
            this.ShowInTaskbar = false;
            this.TopMost = true;
        }

        private void ShortcutForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Hide();
            }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadItems();
        }

        private void ShortcutForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int) Keys.Enter)
            {
                foreach (var shortcut in shortcutList)
                {
                    if (!shortcut.Visible) continue;
                    this.Hide();
                    e.Handled = true;
                    shortcut.Run();
                }
            }
            else if (e.KeyChar == (int) Keys.Escape)
            {
                this.Hide();
                e.Handled = true;
            }
            else if (!textBox.Visible)
            {
                textBox.Text = e.KeyChar.ToString();
                textBox.SelectionStart = textBox.TextLength;
                textBox.Show();
                textBox.Focus();
            }
        }

        private void ShortcutForm_VisibleChanged(object sender, EventArgs e)
        {
            textBox.Text = "";
            textBox.Visible = false;
            this.Refresh();
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox.Text == "")
            {
                textBox.Visible = false;
            }

            foreach (var shortcut in shortcutList)
            {
                shortcut.Visible =
                    shortcut.Caption.IndexOf(textBox.Text.Trim(), StringComparison.OrdinalIgnoreCase) > -1;
            }
        }
    }
}
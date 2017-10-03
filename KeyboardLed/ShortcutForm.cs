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
using System.Linq;

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
                var ctrls = this.Controls
                    .Cast<Control>()
                    .Where(ctrl => ctrl.GetType() == typeof(ShortcutControl))
                    .ToList();
                foreach (var ctrl in ctrls)
                {
                    this.Controls.Remove(ctrl);
                }

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


        private void ShortcutForm_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ShortcutForm_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.TopMost = true;
        }

        private void ShortcutForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.ControlKey)
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

        private void ShortcutForm_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.ControlKey)
            //{
            //    this.Hide();
            //}
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadItems();
        }
    }
}
#region file header

// -----------------------------------------------------------------------------
// Project: KeyboardLed.KeyboardLed
// File:    TransForm.cs
// Author:  Robert.L
// Created: 2013/09/23 14:43
// -----------------------------------------------------------------------------

#endregion

namespace KeyboardLed
{
    #region using statements

    using System;
    using System.Windows.Forms;

    #endregion

    /// <summary>The trans form.</summary>
    public partial class TransForm : Form
    {
        /// <summary>Initializes a new instance of the <see cref="TransForm"/> class.</summary>
        public TransForm()
        {
            InitializeComponent();
        }

        /// <summary>The trans form_ load.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void TransForm_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;

            Native.GetWindowLong(this.Handle, Native.GWL_EXSTYLE);
            Native.SetWindowLong(this.Handle, Native.GWL_EXSTYLE, Native.WS_EX_TRANSPARENT | Native.WS_EX_LAYERED);

            this.TopMost = true;
        }
    }
}
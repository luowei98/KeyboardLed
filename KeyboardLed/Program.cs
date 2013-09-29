#region file header

// -----------------------------------------------------------------------------
// Project: KeyboardLed.KeyboardLed
// File:    Program.cs
// Author:  Robert.L
// Created: 2013/09/23 10:56
// -----------------------------------------------------------------------------

#endregion

namespace KeyboardLed
{
    #region using statements

    using System;
    using System.Windows.Forms;

    #endregion

    /// <summary>The program.</summary>
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception)
            {
                MessageBox.Show("发生了不可预料的错误。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
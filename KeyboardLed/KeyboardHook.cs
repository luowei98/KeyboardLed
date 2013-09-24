#region file header

// -----------------------------------------------------------------------------
// Project: KeyboardLed.KeyboardLed
// File:    KeyboardHook.cs
// Author:  Robert.L
// Created: 2013/09/23 12:48
// -----------------------------------------------------------------------------

#endregion

namespace KeyboardLed
{
    #region using statements

    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    #endregion

    /// <summary>The keyboard hook.</summary>
    public class KeyboardHook
    {
        #region Instance Variables

        /// <summary>
        /// Handle to the hook, need this to unhook and call the next hook
        /// </summary>
        private IntPtr hhook = IntPtr.Zero;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="KeyboardHook"/> class. 
        /// Initializes a new instance of the <see cref="KeyboardHook"/> class and installs the keyboard hook.</summary>
        public KeyboardHook()
        {
            this.Hook();
            this.HookedKeys = new List<Keys>();
        }

        /// <summary>Finalizes an instance of the <see cref="KeyboardHook"/> class. 
        /// Releases unmanaged resources and performs other cleanup operations before the<see cref="KeyboardHook"/> is reclaimed by garbage collection and uninstalls the keyboard hook.</summary>
        ~KeyboardHook()
        {
            this.Unhook();
        }

        #endregion

        #region Delegate Definitions

        /// <summary>
        /// defines the callback type for the hook
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public delegate int KeyboardHookProc(int code, int wParam, ref KeyboardHookStruct lParam);

        #endregion

        #region Events

        /// <summary>
        /// Occurs when one of the hooked keys is pressed
        /// </summary>
        public event KeyEventHandler KeyDown;

        /// <summary>
        /// Occurs when one of the hooked keys is released
        /// </summary>
        public event KeyEventHandler KeyUp;

        #endregion

        #region Delegate Definitions

        /// <summary>
        /// Gets or sets the collections of keys to watch for
        /// </summary>
        public List<Keys> HookedKeys { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Installs the global hook
        /// </summary>
        public void Hook()
        {
            IntPtr hInstance = Native.LoadLibrary("User32");
            this.hhook = Native.SetWindowsHookEx(Native.WH_KEYBOARD_LL, this.HookProc, hInstance, 0);
        }

        /// <summary>
        /// Uninstalls the global hook
        /// </summary>
        public void Unhook()
        {
            Native.UnhookWindowsHookEx(this.hhook);
        }

        /// <summary>The callback for the keyboard hook</summary>
        /// <param name="code">The hook code, if it isn't &gt;= 0, the function shouldn't do anyting</param>
        /// <param name="wParam">The event type</param>
        /// <param name="lParam">The keyhook event information</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int HookProc(int code, int wParam, ref KeyboardHookStruct lParam)
        {
            if (code >= 0)
            {
                var key = (Keys)lParam.VkCode;
                if (this.HookedKeys.Contains(key))
                {
                    var kea = new KeyEventArgs(key);
                    if ((wParam == Native.WM_KEYDOWN || wParam == Native.WM_SYSKEYDOWN) && (this.KeyDown != null))
                    {
                        this.KeyDown(this, kea);
                    }
                    else if ((wParam == Native.WM_KEYUP || wParam == Native.WM_SYSKEYUP) && (this.KeyUp != null))
                    {
                        this.KeyUp(this, kea);
                    }

                    if (kea.Handled)
                    {
                        return 1;
                    }
                }
            }

            return Native.CallNextHookEx(this.hhook, code, wParam, ref lParam);
        }

        #endregion

        /// <summary>The keyboard hook struct.</summary>
        public struct KeyboardHookStruct
        {
            /// <summary>The vk code.</summary>
            public int VkCode;

            /// <summary>The scan code.</summary>
            public int ScanCode;

            /// <summary>The flags.</summary>
            public int Flags;

            /// <summary>The time.</summary>
            public int Time;

            /// <summary>The dw extra info.</summary>
            public int DwExtraInfo;
        }
    }
}
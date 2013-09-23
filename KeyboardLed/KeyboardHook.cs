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
        #region Constant, Structure and Delegate Definitions

        /// <summary>
        /// defines the callback type for the hook
        /// </summary>
        public delegate int keyboardHookProc(int code, int wParam, ref keyboardHookStruct lParam);

        /// <summary>The keyboard hook struct.</summary>
        public struct keyboardHookStruct
        {
            /// <summary>The vk code.</summary>
            public int vkCode;

            /// <summary>The scan code.</summary>
            public int scanCode;

            /// <summary>The flags.</summary>
            public int flags;

            /// <summary>The time.</summary>
            public int time;

            /// <summary>The dw extra info.</summary>
            public int dwExtraInfo;
        }

        #endregion

        #region Instance Variables

        /// <summary>
        /// The collections of keys to watch for
        /// </summary>
        public List<Keys> HookedKeys = new List<Keys>();

        /// <summary>
        /// Handle to the hook, need this to unhook and call the next hook
        /// </summary>
        private IntPtr hhook = IntPtr.Zero;

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

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="KeyboardHook"/> class. 
        /// Initializes a new instance of the <see cref="globalKeyboardHook"/> class and installs the keyboard hook.</summary>
        public KeyboardHook()
        {
            hook();
        }

        /// <summary>Finalizes an instance of the <see cref="KeyboardHook"/> class. 
        /// Releases unmanaged resources and performs other cleanup operations before the<see cref="globalKeyboardHook"/> is reclaimed by garbage collection and uninstalls the keyboard hook.</summary>
        ~KeyboardHook()
        {
            unhook();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Installs the global hook
        /// </summary>
        public void hook()
        {
            IntPtr hInstance = Native.LoadLibrary("User32");
            hhook = Native.SetWindowsHookEx(Native.WH_KEYBOARD_LL, hookProc, hInstance, 0);
        }

        /// <summary>
        /// Uninstalls the global hook
        /// </summary>
        public void unhook()
        {
            Native.UnhookWindowsHookEx(hhook);
        }

        /// <summary>The callback for the keyboard hook</summary>
        /// <param name="code">The hook code, if it isn't &gt;= 0, the function shouldn't do anyting</param>
        /// <param name="wParam">The event type</param>
        /// <param name="lParam">The keyhook event information</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int hookProc(int code, int wParam, ref keyboardHookStruct lParam)
        {
            if (code >= 0)
            {
                var key = (Keys)lParam.vkCode;
                if (HookedKeys.Contains(key))
                {
                    var kea = new KeyEventArgs(key);
                    if ((wParam == Native.WM_KEYDOWN || wParam == Native.WM_SYSKEYDOWN) && (KeyDown != null))
                    {
                        KeyDown(this, kea);
                    }
                    else if ((wParam == Native.WM_KEYUP || wParam == Native.WM_SYSKEYUP) && (KeyUp != null))
                    {
                        KeyUp(this, kea);
                    }

                    if (kea.Handled)
                    {
                        return 1;
                    }
                }
            }

            return Native.CallNextHookEx(hhook, code, wParam, ref lParam);
        }

        #endregion
    }
}
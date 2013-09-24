#region file header

// -----------------------------------------------------------------------------
// Project: KeyboardLed.KeyboardLed
// File:    Native.cs
// Author:  Robert.L
// Created: 2013/09/23 12:54
// -----------------------------------------------------------------------------

#endregion

namespace KeyboardLed
{
    #region using statements

    using System;
    using System.Runtime.InteropServices;

    #endregion

    /// <summary>The native.</summary>
    public static class Native
    {
        #region Constant

        /// <summary>The gwl exstyle.</summary>
        public const int GWL_EXSTYLE = -20;

        /// <summary>The transparent.</summary>
        public const uint WS_EX_TRANSPARENT = 0x20;

        /// <summary>The layered.</summary>
        public const uint WS_EX_LAYERED = 0x80000;

        /// <summary>The keyboard.</summary>
        public const int WH_KEYBOARD_LL = 13;

        /// <summary>The keydown.</summary>
        public const int WM_KEYDOWN = 0x100;

        /// <summary>The keyup.</summary>
        public const int WM_KEYUP = 0x101;

        /// <summary>The syskeydown.</summary>
        public const int WM_SYSKEYDOWN = 0x104;

        /// <summary>The syskeyup.</summary>
        public const int WM_SYSKEYUP = 0x105;

        #endregion

        #region DLL imports

        /// <summary>Sets the windows hook, do the desired event, one of hInstance or threadId must be non-null</summary>
        /// <param name="idHook">The id of the event you want to hook</param>
        /// <param name="callback">The callback.</param>
        /// <param name="hInstance">The handle you want to attach the event to, can be null</param>
        /// <param name="threadId">The thread you want to attach the event to, can be null</param>
        /// <returns>a handle to the desired hook</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(
            int idHook, 
            KeyboardHook.KeyboardHookProc callback, 
            IntPtr hInstance, 
            uint threadId);

        /// <summary>Unhooks the windows hook.</summary>
        /// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
        /// <returns>True if successful, false otherwise</returns>
        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        /// <summary>Calls the next hook.</summary>
        /// <param name="idHook">The hook id</param>
        /// <param name="nCode">The hook code</param>
        /// <param name="wParam">The wparam.</param>
        /// <param name="lParam">The lparam.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(
            IntPtr idHook, 
            int nCode, 
            int wParam, 
            ref KeyboardHook.KeyboardHookStruct lParam);

        /// <summary>Loads the library.</summary>
        /// <param name="szFileName">Name of the library</param>
        /// <returns>A handle to the library</returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string szFileName);

        /// <summary>
        /// Changes an attribute of the specified window. The function also sets the 32-bit (long) value at the specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs..</param>
        /// <param name="nIndex">The zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an integer. To set any other value, specify one of the following values: GWL_EXSTYLE, GWL_HINSTANCE, GWL_ID, GWL_STYLE, GWL_USERDATA, GWL_WNDPROC </param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer. 
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError. </returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        /// <summary>The get window long.</summary>
        /// <param name="hwnd">The hwnd.</param>
        /// <param name="nIndex">The n index.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        [DllImport("user32", EntryPoint = "GetWindowLong")]
        public static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        #endregion
    }
}
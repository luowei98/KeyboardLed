#region file header

// -----------------------------------------------------------------------------
// Project: KeyboardLed.KeyboardLed
// File:    Native.cs
// Author:  Robert.L
// Created: 2013/09/23 12:54
// -----------------------------------------------------------------------------

#endregion

using System.Diagnostics.CodeAnalysis;
using System.Drawing;

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
        public const int WS_EX_TRANSPARENT = 0x20;

        /// <summary>The layered.</summary>
        public const int WS_EX_LAYERED = 0x80000;

        public const int WS_EX_TOOLWINDOW = 0x80;

        public const int WS_EX_NOACTIVATE = 0x08000000;

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

        // for IconHelp ///////////////////////////////////////////////////////
        public const int FILE_ATTRIBUTE_NORMAL = 0x80;
        public const int SHGFI_ICON = 0x000000100;
        public const int SHGFI_ICONLOCATION = 0x000001000;
        public const int SHIL_JUMBO = 0x4;
        ///////////////////////////////////////////////////////////////////////

        #endregion

        #region Struct
        // for IconHelp ///////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            // ReSharper disable once UnusedParameter.Local
            public SHFILEINFO(bool _)
            {
                hIcon = IntPtr.Zero;
                iIcon = 0;
                dwAttributes = 0;
                szDisplayName = "";
                szTypeName = "";
            }

            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public struct IMAGELISTDRAWPARAMS
        {
            public int cbSize;
            public IntPtr himl;
            public int i;
            public IntPtr hdcDst;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int xBitmap; // x offest from the upperleft of bitmap
            public int yBitmap; // y offset from the upperleft of bitmap
            public int rgbBk;
            public int rgbFg;
            public int fStyle;
            public int dwRop;
            public int fState;
            public int Frame;
            public int crEffect;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            private readonly int _Left;
            private readonly int _Top;
            private readonly int _Right;
            private readonly int _Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGEINFO
        {
            private readonly IntPtr hbmImage;
            private readonly IntPtr hbmMask;
            private readonly int Unused1;
            private readonly int Unused2;
            private readonly RECT rcImage;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static implicit operator Point(POINT p)
            {
                return new Point(p.X, p.Y);
            }

            public static implicit operator POINT(Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [ComImport]
        [Guid("46EB5926-582E-4017-9FDF-E8998DAA0950")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        //helpstring("Image List"),
        public interface IImageList
        {
            [PreserveSig]
            int Add(
                IntPtr hbmImage,
                IntPtr hbmMask,
                ref int pi);

            [PreserveSig]
            int ReplaceIcon(
                int i,
                IntPtr hicon,
                ref int pi);

            [PreserveSig]
            int SetOverlayImage(
                int iImage,
                int iOverlay);

            [PreserveSig]
            int Replace(
                int i,
                IntPtr hbmImage,
                IntPtr hbmMask);

            [PreserveSig]
            int AddMasked(
                IntPtr hbmImage,
                int crMask,
                ref int pi);

            [PreserveSig]
            int Draw(
                ref IMAGELISTDRAWPARAMS pimldp);

            [PreserveSig]
            int Remove(
                int i);

            [PreserveSig]
            int GetIcon(
                int i,
                int flags,
                ref IntPtr picon);

            [PreserveSig]
            int GetImageInfo(
                int i,
                ref IMAGEINFO pImageInfo);

            [PreserveSig]
            int Copy(
                int iDst,
                IImageList punkSrc,
                int iSrc,
                int uFlags);

            [PreserveSig]
            int Merge(
                int i1,
                IImageList punk2,
                int i2,
                int dx,
                int dy,
                ref Guid riid,
                ref IntPtr ppv);

            [PreserveSig]
            int Clone(
                ref Guid riid,
                ref IntPtr ppv);

            [PreserveSig]
            int GetImageRect(
                int i,
                ref RECT prc);

            [PreserveSig]
            int GetIconSize(
                ref int cx,
                ref int cy);

            [PreserveSig]
            int SetIconSize(
                int cx,
                int cy);

            [PreserveSig]
            int GetImageCount(
                ref int pi);

            [PreserveSig]
            int SetImageCount(
                int uNewCount);

            [PreserveSig]
            int SetBkColor(
                int clrBk,
                ref int pclr);

            [PreserveSig]
            int GetBkColor(
                ref int pclr);

            [PreserveSig]
            int BeginDrag(
                int iTrack,
                int dxHotspot,
                int dyHotspot);

            [PreserveSig]
            int EndDrag();

            [PreserveSig]
            int DragEnter(
                IntPtr hwndLock,
                int x,
                int y);

            [PreserveSig]
            int DragLeave(
                IntPtr hwndLock);

            [PreserveSig]
            int DragMove(
                int x,
                int y);

            [PreserveSig]
            int SetDragCursorImage(
                ref IImageList punk,
                int iDrag,
                int dxHotspot,
                int dyHotspot);

            [PreserveSig]
            int DragShowNolock(
                int fShow);

            [PreserveSig]
            int GetDragImage(
                ref POINT ppt,
                ref POINT pptHotspot,
                ref Guid riid,
                ref IntPtr ppv);

            [PreserveSig]
            int GetItemFlags(
                int i,
                ref int dwFlags);

            [PreserveSig]
            int GetOverlayImage(
                int iOverlay,
                ref int piIndex);
        };
        ///////////////////////////////////////////////////////////////////////

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

        // for IconHelp ///////////////////////////////////////////////////////
        //see the msdn link below for details on the parameters for this api function
        //https://msdn.microsoft.com/en-us/library/windows/desktop/bb762149%28v=vs.85%29.aspx
        [DllImport("Shell32.dll", EntryPoint = "SHDefExtractIconW")]
        public static extern int SHDefExtractIconW(
            [MarshalAs(UnmanagedType.LPTStr)]string pszIconFile,
            int iIndex,
            uint uFlags,
            ref IntPtr phiconLarge,
            ref IntPtr phiconSmall,
            uint nIconSize);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SHGetFileInfo(
            string pszPath,
            int dwFileAttributes,
            ref SHFILEINFO shinfo,
            uint cbfileInfo,
            int uFlags);

        /// SHGetImageList is not exported correctly in XP.  See KB316931
        /// http://support.microsoft.com/default.aspx?scid=kb;EN-US;Q316931
        /// Apparently (and hopefully) ordinal 727 isn't going to change.
        ///
        [DllImport("shell32.dll", EntryPoint = "#727")]
        public static extern int SHGetImageList(
            int iImageList,
            ref Guid riid,
            out IImageList ppv
            );

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);
        ///////////////////////////////////////////////////////////////////////

        #endregion
    }
}
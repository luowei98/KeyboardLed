// 
// Created by luo.wei on 2017/09/30.
// 

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace KeyboardLed
{
    public class IconHelp
    {
        public static Icon GetHighestIcon(string path, int index = 0)
        {
            // get 32*32 & 256*256 size icon
            const uint size = (uint)((32 << 16) | (256 & 0xffff));

            var hLrgIcon = IntPtr.Zero;
            var hSmlIcon = IntPtr.Zero;

            var result = Native.SHDefExtractIconW(path, index, 0, ref hLrgIcon, ref hSmlIcon, size);
            if (result != 0) return null;

            //if the large and/or small icons where created in the unmanaged memory successfuly then create
            //a clone of them in the managed icons and delete the icons in the unmanaged memory.
            if (hLrgIcon != IntPtr.Zero)
            {
                var largeIcon = (Icon)Icon.FromHandle(hLrgIcon).Clone();
                Native.DestroyIcon(hLrgIcon);
                return largeIcon;
            }
            if (hSmlIcon != IntPtr.Zero)
            {
                var smallIcon = (Icon)Icon.FromHandle(hSmlIcon).Clone();
                Native.DestroyIcon(hSmlIcon);
                return smallIcon;
            }

            return null;
        }

        public static Icon GetHighestExtensionIcon(string path)
        {
            var shinfo = new Native.SHFILEINFO(true);
            const int flags = Native.SHGFI_ICON;

            var res = Native.SHGetFileInfo(path, Native.FILE_ATTRIBUTE_NORMAL, ref shinfo, (uint)Marshal.SizeOf(shinfo), flags);
            if (res == IntPtr.Zero)
            {
                return null;
            }
            var iconIndex = shinfo.iIcon;

            // Get the System IImageList object from the Shell:
            var iidImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");

            Native.IImageList iml;
            Native.SHGetImageList(Native.SHIL_JUMBO, ref iidImageList, out iml);

            var hIcon = IntPtr.Zero;
            const int ildTransparent = 1;
            iml.GetIcon(iconIndex, ildTransparent, ref hIcon);

            var icon = (Icon)Icon.FromHandle(hIcon).Clone();
            Native.DestroyIcon(hIcon);

            return icon;
        }

        public static Icon GetFolderIcon(string dir)
        {
            var shinfo = new Native.SHFILEINFO(true);
            const int flags = Native.SHGFI_ICONLOCATION;

            var res = Native.SHGetFileInfo(dir, Native.FILE_ATTRIBUTE_NORMAL, ref shinfo, (uint)Marshal.SizeOf(shinfo), flags);
            if (res == IntPtr.Zero)
            {
                return null;
            }

            return GetHighestIcon(shinfo.szDisplayName, shinfo.iIcon);
        }

        public static Icon GetDefaultIcon()
        {
            var shinfo = new Native.SHFILEINFO(true);
            const int flags = Native.SHGFI_ICONLOCATION;

            var res = Native.SHGetFileInfo(@"C:\", Native.FILE_ATTRIBUTE_NORMAL, ref shinfo, (uint)Marshal.SizeOf(shinfo), flags);
            if (res == IntPtr.Zero)
            {
                return null;
            }

            return GetHighestIcon(shinfo.szDisplayName, 2);
        }

    }
}
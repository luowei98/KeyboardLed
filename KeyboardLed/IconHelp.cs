// 
// Created by luo.wei on 2017/09/30.
// 

using System;
using System.Drawing;
using System.Runtime.InteropServices;

//using System.Drawing.IconLib;

namespace KeyboardLed
{
    public class IconHelp
    {
        //public static Icon GetHighestIcon(string path, int index = 0)
        //{
        //    var icons = new MultiIcon();
        //    icons.Load(path);

        //    if (icons.Count <= index)
        //    {
        //        return null;
        //    }

        //    var icon = icons[index];
        //    IconImage image = null;
        //    foreach (var i in icon)
        //    {
        //        if (image != null && i.Size.Width < image.Size.Width)
        //        {
        //            continue;
        //        }
        //        image = i;
        //    }

        //    return image?.Icon;

        //    //return null;
        //}

        //see the msdn link below for details on the parameters for this api function
        //https://msdn.microsoft.com/en-us/library/windows/desktop/bb762149%28v=vs.85%29.aspx
        [DllImport("Shell32.dll", EntryPoint = "SHDefExtractIconW")]
        private static extern int SHDefExtractIconW(
            [MarshalAs(UnmanagedType.LPTStr)]string pszIconFile,
            int iIndex,
            uint uFlags,
            ref IntPtr phiconLarge,
            ref IntPtr phiconSmall,
            uint nIconSize);

        [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        public static Icon GetHighestIcon(string path, int index = 0)
        {
            var size = (uint)((32 << 16) | (128 & 0xffff));

            var hLrgIcon = IntPtr.Zero;
            var hSmlIcon = IntPtr.Zero;

            var result = SHDefExtractIconW(path, index, 0, ref hLrgIcon, ref hSmlIcon, size);
            if (result == 0)
            {
                //if the large and/or small icons where created in the unmanaged memory successfuly then create
                //a clone of them in the managed icons and delete the icons in the unmanaged memory.
                if (hLrgIcon != IntPtr.Zero)
                {
                    var largeIcon = (Icon)Icon.FromHandle(hLrgIcon).Clone();
                    DestroyIcon(hLrgIcon);
                    return largeIcon;
                }
                if (hSmlIcon != IntPtr.Zero)
                {
                    var smallIcon = (Icon)Icon.FromHandle(hSmlIcon).Clone();
                    DestroyIcon(hSmlIcon);
                    return smallIcon;
                }
            }
            return null;
        }

    }
}
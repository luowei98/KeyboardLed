// 
// Created by luo.wei on 2017/09/30.
// 

using System.Drawing;
using System.Drawing.IconLib;

namespace KeyboardLed
{
    public class IconHelp
    {
        public static Icon GetHighestIcon(string path, int index = 0)
        {
            var icons = new MultiIcon();
            icons.Load(path);

            if (icons.Count <= index)
            {
                return null;
            }

            var icon = icons[index];
            IconImage image = null;
            foreach (var i in icon)
            {
                if (image != null && i.Size.Width < image.Size.Width)
                {
                    continue;
                }
                image = i;
            }

            return image?.Icon;
        }
    }
}
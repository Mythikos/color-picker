using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ColorPicker.Classes.Static
{
    public static class WindowsLibExt
    {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point cursor);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        /// <summary>
        /// Gets the pixel at the cursor location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Color GetPixelAt(Point location)
        {
            Bitmap pixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using (Graphics graphicsFromImage = Graphics.FromImage(pixel))
            {
                using (Graphics graphicsFromHandle = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = graphicsFromHandle.GetHdc();
                    IntPtr hDC = graphicsFromImage.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    graphicsFromImage.ReleaseHdc();
                    graphicsFromHandle.ReleaseHdc();
                }
            }

            return pixel.GetPixel(0, 0);
        }
    }
}

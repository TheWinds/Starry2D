using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Starry2D.Engine
{
    /// <summary>
    /// GDI 绘图接口，用户加速
    /// </summary>
    public class GDIUtils
    {
        /// <summary>
        /// 使用 BitBlt复制Bitmap来提高DrawImage的速度
        /// </summary>
        /// <param name="baseGraphics">Graphics</param>
        /// <param name="bitmap">位图</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void DrawImage(Graphics baseGraphics, Bitmap bitmap, int width, int height)
        {
            try
            {
                var hdc = baseGraphics.GetHdc();
                var memdc = CreateCompatibleDC(hdc);
                var a = bitmap.GetHbitmap();
                SelectObject(memdc, a);
                //var memDC = Graphics.FromHdc(memdc);
                BitBlt(hdc, 0, 0, width, height, memdc, 0, 0, 13369376);
                baseGraphics.ReleaseHdc();
                DeleteDC(memdc);
                DeleteObject(a);
            }
            catch (Exception)
            {


            }

        }

        #region DLL import

        [DllImport("gdi32.dll")]
        public static extern int BitBlt(

           IntPtr hdcDest,     // handle to destination DC (device context)

           int nXDest,         // x-coord of destination upper-left corner

           int nYDest,         // y-coord of destination upper-left corner

           int nWidth,         // width of destination rectangle

           int nHeight,        // height of destination rectangle

           IntPtr hdcSrc,      // handle to source DC

           int nXSrc,          // x-coordinate of source upper-left corner

           int nYSrc,          // y-coordinate of source upper-left corner

           System.Int32 dwRop  // raster operation code

     );

        [DllImport("gdi32.dll")]
        static public extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        static public extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [DllImport("gdi32.dll")]
        static public extern IntPtr DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        static public extern bool DeleteObject(IntPtr hObject);

        #endregion
    }
}

using ChessWindowsForms;
using ChessWindowsForms.View.Helper;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChessWindowsForms.View.Helper
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    public static class CursorUtility
    {
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr handle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        public static Cursor CreateCursor(Bitmap bm, int xHotspot, int yHotspot)
        {
            IntPtr ptr = bm.GetHicon();

            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = xHotspot;
            tmp.yHotspot = yHotspot;
            tmp.fIcon = false;

            IntPtr cursorPtr = CreateIconIndirect(ref tmp);

            if (tmp.hbmColor != IntPtr.Zero)
                DeleteObject(tmp.hbmColor);
            if (tmp.hbmMask != IntPtr.Zero)
                DeleteObject(tmp.hbmMask);
            if (ptr != IntPtr.Zero)
                DestroyIcon(ptr);

            return new Cursor(cursorPtr);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundWindowMover
{
    internal class PInvoke
    {
        // P/Invoke for Win32 APIs
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_NOACTIVATE = 0x0010;

        // Struct for the window's rectangle
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // Struct for the point coordinates
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        public const uint RDW_INVALIDATE = 0x0001;
        public const uint RDW_UPDATENOW = 0x0100;
        public const uint RDW_ALLCHILDREN = 0x0080;

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);


        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hWnd);
    }
}

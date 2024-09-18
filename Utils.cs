using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWindowMover
{
    internal class Utils
    {

        // Check if a specific key is pressed
        public static bool IsKeyDown(Keys key)
        {
            return Control.ModifierKeys.HasFlag(key);
        }

        // Force a redraw of the window
        public static void ForceRedrawWindow(IntPtr hWnd)
        {
            PInvoke.RedrawWindow(hWnd, IntPtr.Zero, IntPtr.Zero, PInvoke.RDW_INVALIDATE | PInvoke.RDW_UPDATENOW | PInvoke.RDW_ALLCHILDREN);
        }

        // Force a redraw using InvalidateRect and UpdateWindow
        public static void ForceRedrawWindowUsingInvalidate(IntPtr hWnd)
        {
            // Invalidate the entire window (null rect) and force it to repaint
            PInvoke.InvalidateRect(hWnd, IntPtr.Zero, true);
            PInvoke.UpdateWindow(hWnd);
        }
    }
}

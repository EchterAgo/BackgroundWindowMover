using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BackgroundWindowMover.PInvoke;
using static BackgroundWindowMover.Utils;

namespace BackgroundWindowMover
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            updateTimer.Start();
        }

        private static IntPtr lastWindow = IntPtr.Zero;
        private static PInvoke.Point lastMousePos;

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            // Check if Alt and Ctrl are held
            if (IsKeyDown(Keys.Alt) && IsKeyDown(Keys.Control))
            {
                PInvoke.Point cursorPos;
                if (GetCursorPos(out cursorPos))
                {
                    // If this is the first detection or the window handle is invalid, get the window under the mouse
                    if (lastWindow == IntPtr.Zero)
                    {
                        lastWindow = WindowFromPoint(cursorPos);

                        // Store the current mouse position for calculating movement offsets
                        lastMousePos = cursorPos;
                    }

                    if (lastWindow != IntPtr.Zero)
                    {
                        // Get the window's current position
                        RECT rect;
                        if (GetWindowRect(lastWindow, out rect))
                        {
                            // Calculate how much the mouse has moved since the last position
                            int deltaX = cursorPos.X - lastMousePos.X;
                            int deltaY = cursorPos.Y - lastMousePos.Y;

                            // Move the window by the same amount
                            int newX = rect.Left + deltaX;
                            int newY = rect.Top + deltaY;
                            int width = rect.Right - rect.Left;
                            int height = rect.Bottom - rect.Top;

                            // Move the window without bringing it to the front
                            SetWindowPos(lastWindow, IntPtr.Zero, newX, newY, width, height, SWP_NOZORDER | SWP_NOACTIVATE);
                            //SetWindowPos(lastWindow, IntPtr.Zero, newX, newY, width, height, SWP_NOACTIVATE);
                            //SetWindowPos(lastWindow, IntPtr.Zero, newX, newY, width, height, SWP_NOZORDER);
                            //SetWindowPos(lastWindow, IntPtr.Zero, newX, newY, width, height, 0);
                            ForceRedrawWindowUsingInvalidate(lastWindow);

                            // Update the last mouse position
                            lastMousePos = cursorPos;
                        }
                    }
                }
            }
            else
            {
                // Reset the last window when Alt+Ctrl is released
                lastWindow = IntPtr.Zero;
            }
        }
    }
}

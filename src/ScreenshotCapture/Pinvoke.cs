using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ScreenshotCapture
{
    internal static class PInvoke
    {
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;


        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    }
}

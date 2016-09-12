using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using ScreenshotCapture.Lib;

namespace ScreenshotCapture
{
    internal static class Program
    {

        private static readonly string Root = Path.Combine(Environment.CurrentDirectory, "Screenshots");

        static Program()
        {
            if (!Directory.Exists(Root))
                Directory.CreateDirectory(Root);
        }

        private static void Main(string[] args)
        {

#if !DEBUG
            var handle = PinvokeHelper.GetConsoleWindow();

            // Hide
            PinvokeHelper.ShowWindow(handle, PinvokeHelper.SW_HIDE);      
#endif
            
            for (var i = 0; i < int.MaxValue; i++)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var bmp = ScreenCapture.GetImage();

                if (BitmapDetector.IsBlank(bmp))
                    continue;

                stopWatch.Stop();
                Console.WriteLine("{0}({1:g}ms)", bmp, stopWatch.Elapsed);

                Thread.Sleep(1000);
            }

            #if !DEBUG
                // Show
                PinvokeHelper.ShowWindow(handle, PinvokeHelper.SW_SHOW);
            #endif
        }
    }
}
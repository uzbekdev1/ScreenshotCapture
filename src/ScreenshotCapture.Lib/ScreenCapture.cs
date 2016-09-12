using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenshotCapture.Lib
{
    public class ScreenCapture
    {
        public static string GetImage(bool isAll=false)
        {
            if (isAll)
            {
                var screens = Screen.AllScreens;
                var files = new string[screens.Length];

                for (var i = 0; i < screens.Length; i++)
                {
                    files[i] = GetImage(screens[i]);
                }

                return CombiningImages.MergeMultipleImages(files);
            }
            else
            {
                return GetImage(Screen.PrimaryScreen);
            }
        }

        private static string GetImage(Screen screen)
        {
            //generate random temp image path
            var path = Path.Combine(Settings.ScreenshotPath, Path.ChangeExtension(Path.GetRandomFileName(), ".png"));

            //Create a new bitmap.
            using (var bmpScreenshot = new Bitmap(screen.Bounds.Width, screen.Bounds.Height,
                PixelFormat.Format32bppArgb))
            {

                // Create a graphics object from the bitmap.
                var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

                // Take the screenshot from the upper left corner to the right bottom corner.
                gfxScreenshot.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, screen.Bounds.Size, CopyPixelOperation.SourceCopy);

                // Save the screenshot to the specified path that the user has chosen.
                bmpScreenshot.Save(path, ImageFormat.Png);

                return path;
            }
        }
    }
}
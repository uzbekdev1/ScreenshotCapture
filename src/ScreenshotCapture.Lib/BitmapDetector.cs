using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ScreenshotCapture.Lib
{
    /// <summary>
    ///     Detecting Blank Images
    ///     http://www.chinhdo.com/20080910/detect-blank-images/
    /// </summary>
    public static class BitmapDetector
    {
        /// <summary>
        ///     check is blank
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static bool IsBlank(Bitmap img)
        {
            var stdDev = GetStdDev(img);
            var tolerance = 100000;

            return stdDev < tolerance;
        }

        /// <summary>
        ///     Get the standard deviation of pixel values.
        /// </summary>
        /// <param name="bitmap">Name of the image file.</param>
        /// <returns>Standard deviation.</returns>
        public static double GetStdDev(Bitmap bitmap)
        {
            double total = 0, totalVariance = 0;
            var count = 0;
            double stdDev = 0;

            // First get all the bytes
            var bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                bitmap.PixelFormat);
            var stride = bmData.Stride;
            var Scan0 = bmData.Scan0;

            unsafe
            {
                var p = (byte*) (void*) Scan0;
                var nOffset = stride - bitmap.Width*3;
                for (var y = 0; y < bitmap.Height; ++y)
                {
                    for (var x = 0; x < bitmap.Width; ++x)
                    {
                        count++;

                        var blue = p[0];
                        var green = p[1];
                        var red = p[2];

                        var pixelValue = Color.FromArgb(0, red, green, blue).ToArgb();
                        total += pixelValue;
                        var avg = total/count;
                        totalVariance += Math.Pow(pixelValue - avg, 2);
                        stdDev = Math.Sqrt(totalVariance/count);

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            bitmap.UnlockBits(bmData);

            return stdDev;
        }
    }
}
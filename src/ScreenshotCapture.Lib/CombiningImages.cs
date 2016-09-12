using System.IO;
using ImageMagick;

namespace ScreenshotCapture.Lib
{
    public static class CombiningImages
    {
        public static string MergeMultipleImages(string[] files)
        {
            using (var images = new MagickImageCollection())
            {
                foreach (var file in files)
                {
                    // Add the image
                    var image = new MagickImage(new FileInfo(file));

                    images.Add(image);
                }

                // Create a mosaic from both images
                using (var result = images.Mosaic())
                {
                    // Save the result
                    var path = Path.Combine(Settings.ScreenshotPath, Path.ChangeExtension(Path.GetRandomFileName(), ".png"));

                    result.Write(path);

                    return path;
                }
            }
        }

    }
}

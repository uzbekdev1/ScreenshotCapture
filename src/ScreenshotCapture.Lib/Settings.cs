using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScreenshotCapture.Lib
{
   internal static class Settings
   {
       public static readonly string ScreenshotPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().FullName),"Screenshots");
   }
}

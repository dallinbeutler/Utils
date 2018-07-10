using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Utils
{


   public static class Util
   {
      public static string GetRandomFile(string path)
      {
         string file = null;
         if (!string.IsNullOrEmpty(path))
         {
            var extensions = new string[] { ".png", ".jpg", ".gif" };
            try
            {
               var di = new DirectoryInfo(path);
               var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower()));
               Random R = new Random();
               file = rgFiles.ElementAt(R.Next(0, rgFiles.Count())).FullName;
            }
            // probably should only catch specific exceptions
            // throwable by the above methods.
            catch { }
         }
         return file;
      }
      //this function takes a function returning bool and calls it either until it succeeds or the time is up
      //use () => myfunc( v1,v2,v3) in task spot if your function takes arguments
      public static bool RetryUntilSuccessOrTimeout(Func<bool> task, TimeSpan timeSpan)
      {
         bool success = false;
         int elapsed = 0;
         while ((!success) && (elapsed < timeSpan.TotalMilliseconds))
         {
            Thread.Sleep(1000);
            elapsed += 1000;
            success = task();
         }
         return success;
      }
      public static string ListToString(string separator, List<object> inString)
      {
         return String.Join(separator, inString.ToArray());
      }
      // generate random hash for unique filename
      public static string ReturnUniqueValue(string ID)
      {
         var result = default(byte[]);

         using (var stream = new MemoryStream())
         {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
               writer.Write(DateTime.Now.Ticks);
               writer.Write(ID);
            }

            stream.Position = 0;

            using (var hash = System.Security.Cryptography.SHA256.Create())
            {
               result = hash.ComputeHash(stream);
            }
         }

         var text = new string[20];

         for (var i = 0; i < text.Length; i++)
         {
            text[i] = result[i].ToString("x2");
         }

         return string.Concat(text);
      }
      /// <summary>
      /// This will check a filename in a path for duplicates. if there is a duplicate, 
      /// it will append a number 
      /// </summary>
      /// <param name="folder"></param>
      /// <param name="infilename"></param>
      /// <returns></returns>
      public static string IndexedFilename(string folder, string infilename)
      {
         if (!File.Exists(folder + infilename))
         {
            return infilename;
         }

         var namesplit = infilename.Split('.');
         int ix = 1;
         string filename = null;
         do
         {
            ix++;
            filename = String.Format("{0}({1}).{2}", namesplit[0], ix, namesplit[1]);
         } while (File.Exists(folder + filename));
         return filename;
      }
      public static void StartFile(string filePath)
      {
         System.Diagnostics.Process.Start(filePath);
      }
      public static string GetEnumDescription(Enum value)
      {
         FieldInfo fi = value.GetType().GetField(value.ToString());

         DescriptionAttribute[] attributes =
             (DescriptionAttribute[])fi.GetCustomAttributes(
             typeof(DescriptionAttribute),
             false);

         if (attributes != null &&
             attributes.Length > 0)
         {
            return attributes[0].Description;
         }
         else
         {
            return value.ToString();
         }
      }



      // Finds next power of two 
      // for n. If n itself is a 
      // power of two then returns n
      public static int nextPowerOf2(int n)
      {
         n--;
         n |= n >> 1;
         n |= n >> 2;
         n |= n >> 4;
         n |= n >> 8;
         n |= n >> 16;
         n++;

         return n;
      }
      public static int GetBigger(int x, int y)
      {
         return x > y ? x : y;
      }
      public static float GetBigger(float x, float y)
      {
         return x > y ? x : y;
      }
      public static double GetBigger(double x, double y)
      {
         return x > y ? x : y;
      }
      public static long GetBigger(long x, long y)
      {
         return x > y ? x : y;
      }
   }

   /// <summary>
   /// Example:
   /// using (new TimeIt("Outer scope"))
   ///{
   ///using (new TimeIt("Inner scope A"))
   ///{
   ///DoSomeWork("A");
   ///}
   ///using (new TimeIt("Inner scope B"))
   ///{
   ///DoSomeWork("B");
   ///}
   ///Cleanup();
   ///}
   /// </summary>
   public class TimeIt : IDisposable
   {
      private readonly string _name;
      private readonly Stopwatch _watch;
      public TimeIt(string name)
      {
         _name = name;
         _watch = Stopwatch.StartNew();
      }
      public void Dispose()
      {
         _watch.Stop();
         Console.WriteLine("{0} took {1}", _name, _watch.Elapsed);
      }
   }

   //Creates a scope for a console foreground color. When disposed, will return to 
   //  the previous Console.ForegroundColor
   public class ConsoleColors : IDisposable
   {
      private readonly System.ConsoleColor _previousColor;
      public ConsoleColors(System.ConsoleColor color)
      {
         _previousColor = Console.ForegroundColor;
         Console.ForegroundColor = color;
      }
      public void Dispose()
      {
         Console.ForegroundColor = _previousColor;

      }
   }


}

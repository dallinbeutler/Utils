using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Utils
{
   public static partial class Util
   {
      public static void StartFile(string filePath)
      {
         System.Diagnostics.Process.Start(filePath);
      }

      public static List<string> GetAllFiles(string path)
      {
         var files = Directory.EnumerateFiles(path).ToList();

         var dirs = Directory.EnumerateDirectories(path);

         foreach (var dir in dirs)
         {
            files.AddRange(GetAllFiles(dir));
         }
         return files;
      }
      public static List<string> GetAllFolders(string path)
      {
         var dirs = Directory.EnumerateDirectories(path);
         var outFolders = dirs.ToList();
         foreach (var dir in dirs)
         {
            outFolders.AddRange(GetAllFolders(dir));
         }
         return outFolders;
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
   }
}

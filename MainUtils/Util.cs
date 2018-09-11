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


   public static partial class Util
   {


      //this function takes a function returning bool and calls it either until it succeeds or the time is up
      //use () => myfunc( v1,v2,v3) in task spot if your function takes arguments
      public static bool RetryUntilSuccessOrTimeout(this Func<bool> task, TimeSpan timeSpan)
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

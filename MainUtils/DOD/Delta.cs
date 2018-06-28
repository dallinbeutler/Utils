using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;

namespace UtilsMain.DOD
{
   static class Delta
   {
      private static Stopwatch stopwatch = new Stopwatch();
      static long delta = 0;

      //30 fps
      const int frameRate = 32;
      static Delta()
      {
         Observable.Interval(new TimeSpan(0, 0, 0, 32)).Subscribe(x =>
         {
            delta = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
         });
      }
   }
}

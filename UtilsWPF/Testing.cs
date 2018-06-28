using System.Collections.Generic;
using Utils.DOD;
using UtilsMain.DOD;

namespace Utils
{

   public static class Program
   {
      private static DataStream<int> Entities;
      public static void Main()
      {
         MainLoop loop = new MainLoop();
         int two = FoldMapUCase.Zumbro.AlwaysTwo;
         loop.Update();

         dynamic poo = 2;
         if (poo is int i)
         {
            i++;
         }
      }
   }

}

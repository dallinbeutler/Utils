using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static Utils.Extensions.ExtEnumerables;

namespace DOD
{
   
   class Atlas
   {

      class PackImage
      {
         public Bitmap Image;
         public int LocX;
         public int LocY;
         public int activeW;
         public int activeH;
         public int activeXOffset;
         public int activeYOffset;
      }
      List<PackImage> images = new List<PackImage>();

      void UsedArea(Bitmap image)
      {
         int left = -1, top = -1, right =-1, bottom = -1;
      
         for(int xi = 0; xi< image.Width; xi++)
         {
            for(int yi = 0; yi< image.Height; yi++)
            {
               if (image.GetPixel(xi, yi).A == 0)
                  continue;
               else
               {
                  left = xi;
                  goto step1;
               }
            }
         }
         step1:
         for (int xi = image.Width-1; xi >= 0; xi--)
         {
            for (int yi = 0; yi < image.Height; yi++)
            {
               if (image.GetPixel(xi, yi).A == 0)
                  continue;
               else
               {
                  right = xi;
                  goto step2;
               }
            }
         }
         step2:
         for (int yi = 0; yi < image.Height; yi++)
         {
            for (int xi = 0; xi < image.Width; xi++)
            {
               if (image.GetPixel(xi, yi).A == 0)
                  continue;
               else
               {
                  top = yi;
                  goto step3;
               }
            }
         }
         step3:
         for (int yi = image.Height-1; yi >= 0; yi--)
         {
            for (int xi = 0; xi < image.Width; xi++)
            {
               if (image.GetPixel(xi, yi).A == 0)
                  continue;
               else
               {
                  bottom = yi;
                  goto step4;
               }
            }
         }
         step4:
         {
            if (left == -1 )
            {
               Console.WriteLine("EMPTY!");
            }
            else
            {
               Console.WriteLine("Doing stuff here!");
            }
         }
      }
      void Pack()
      {
         var sorted = images.Sort(x=> (x.Image.Width*x.Image.Height), SortDirection.Descending);
         //var totalPixels = (int)Math.Sqrt( images.Sum(x => x.Height * x.Width));
         //var largestSquare = Utils.Util.nextPowerOf2(totalPixels);
         int Width = 2;
         int Height = 2;
         foreach (var i in sorted)
         {
            
         }
      }
   }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
   public static partial class Util
   {

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

      public static T GetBigger<T>(this T Left, T Right) where T :
struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         if (Tools<T>.GreaterThan(Left, Right))
            return Left;
         else
            return Right;
      }

      public static T GetSmaller<T>(this T Left, T Right) where T :
struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         if (Tools<T>.GreaterThan(Left, Right))
            return Right;
         else
            return Left;
      }
//      public static void LerpMe<T>(this T Value, T Goal, T Rate) where T :
//struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
//      {
//         var difference = Tools<T>.Subtract(Goal,Value);
//         if (difference.GreaterThan(Rate))
//            Value.Add(Rate);
//         else
//            return difference;
//      }
//      public static T Lerp<T>(T Value, T Goal, T Rate) where T :
//struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
//      {
//         var difference = Tools<T>.Subtract(Goal, Value);
//         if (difference.GreaterThan(Rate))
//            return Rate;
//         else
//            return difference;
//      }

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
}

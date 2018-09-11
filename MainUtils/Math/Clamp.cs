using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
   public static partial class Util
   {
      public static int Clamp(this int value, int min, int max)
      {
         if (value < min)
            return min;
         if (value > max)
            return max;
         return value;
         
      }
      public static T Clamp<T>(this T value, T min, T max) where T :
 struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         if (Tools<T>.LessThan(value, min))
            return min;
         if (Tools<T>.GreaterThan(value, max))
            return max;
         return value;
      }
   }
}

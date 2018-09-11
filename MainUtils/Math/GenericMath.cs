using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Utils
{
   public static class GenericMath
   {
      //returns T
      public static T Add<T>(this T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         return Tools<T>.Add(a, b);
      }
      public static T Subtract<T>(this T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         return Tools<T>.Subtract(a, b);
      }
      public static T Multiply<T>(this T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         return Tools<T>.Multiply(a, b);
      }
      public static T Divide<T>(this T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         return Tools<T>.Divide(a, b);
      }
      public static T XOR<T>(this T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         return Tools<T>.XOR(a, b);
      }

      // returns bool
      public static bool LessThan<T>(this T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         return Tools<T>.LessThan(a, b);
      }
      public static bool LessThanOrEqual<T>(this T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         return Tools<T>.LessThanOrEqual(a, b);
      }
      public static bool GreaterThan<T>(this T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         return Tools<T>.GreaterThan(a, b);
      }
      public static bool GreaterThanOrEqual<T>(this T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         return Tools<T>.GreaterThanOrEqual(a, b);
      }
      public static bool Equal<T>(this T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         return Tools<T>.Equal(a, b);
      }

      public static void SwapNoTemp<T>(ref T a, ref T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         if (a.Equals(b))
            return;
         a = XOR(a, b);
         b = XOR(a, b);
         a = XOR(a, b);
      }
      public static T Clamp<T>(this T value, T min, T max) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         if (value.GreaterThan(min))
            return min;
         if (value.LessThan(max))
            return max;
         return value;
      }
      public static void SwapTemp<T>(ref T a, ref T b)
      {
         var t = a;
         a = b;
         b = t;
      }
      public static void SwapTemp<T>(this T a, ref T b)
      {
         var t = a;
         a = b;
         b = t;
      }
      public static void Sort<T>(this ref T a, ref T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      {
         if (a.CompareTo(b) > 0)
            SwapTemp(ref a, ref b);
      }

      //   public static T nextPowerOf2<T>(this T val) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
      //{
      //   Tools<T>.RightShift(val,);
      //}
   }

   //http://www.yoda.arachsys.com/csharp/genericoperators.html
   internal static class Tools<T> where T :
  struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
   {

      internal static Func<T, T, T> Add;
      internal static Func<T, T, T> Subtract;
      internal static Func<T, T, T> Multiply;
      internal static Func<T, T, T> Divide;
      internal static Func<T, T, T> XOR;
      internal static Func<T, int, T> RightShift;
      internal static Func<T, T, bool> LessThan;
      internal static Func<T, T, bool> LessThanOrEqual;
      internal static Func<T, T, bool> GreaterThan;
      internal static Func<T, T, bool> GreaterThanOrEqual;
      internal static Func<T, T, bool> Equal;
      //internal static Func<T, T, T> diff;


      static Tools()
      {
         // declare the parameters
         ParameterExpression paramA =
         System.Linq.Expressions.Expression.Parameter(typeof(T), "a"),
             paramB = System.Linq.Expressions.Expression.Parameter(typeof(T), "b");
         // add the parameters together
         BinaryExpression body = System.Linq.Expressions.Expression.Add(paramA, paramB);
         // compile it
         Add = System.Linq.Expressions.Expression.Lambda<Func<T, T, T>>
                 (Expression.Add(paramA, paramB), paramA, paramB).Compile();
         Subtract = System.Linq.Expressions.Expression.Lambda<Func<T, T, T>>
                 (Expression.Subtract(paramA, paramB), paramA, paramB).Compile();
         Multiply = System.Linq.Expressions.Expression.Lambda<Func<T, T, T>>
                 (Expression.Multiply(paramA, paramB), paramA, paramB).Compile();
         Divide = System.Linq.Expressions.Expression.Lambda<Func<T, T, T>>
                 (Expression.Divide(paramA, paramB), paramA, paramB).Compile();
         XOR = System.Linq.Expressions.Expression.Lambda<Func<T, T, T>>
                 (Expression.ExclusiveOr(paramA, paramB), paramA, paramB).Compile();
         LessThan = System.Linq.Expressions.Expression.Lambda<Func<T, T, bool>>
                 (Expression.LessThan(paramA, paramB), paramA, paramB).Compile();
         LessThanOrEqual = System.Linq.Expressions.Expression.Lambda<Func<T, T, bool>>
                 (Expression.LessThanOrEqual(paramA, paramB), paramA, paramB).Compile();
         GreaterThan = System.Linq.Expressions.Expression.Lambda<Func<T, T, bool>>
                (Expression.GreaterThan(paramA, paramB), paramA, paramB).Compile();
         GreaterThanOrEqual = System.Linq.Expressions.Expression.Lambda<Func<T, T, bool>>
                (Expression.GreaterThanOrEqual(paramA, paramB), paramA, paramB).Compile();
         Equal = System.Linq.Expressions.Expression.Lambda<Func<T, T, bool>>
                (Expression.Equal(paramA, paramB), paramA, paramB).Compile();
         RightShift = System.Linq.Expressions.Expression.Lambda<Func<T, int, T>>
             (Expression.RightShift(paramA, paramB), paramA, paramB).Compile();
         //diff= System.Linq.Expressions.Expression.Lambda<Func<T, T, T>>
         //           (Expression.d(paramA, paramB), paramA, paramB).Compile();

      }
   }
}

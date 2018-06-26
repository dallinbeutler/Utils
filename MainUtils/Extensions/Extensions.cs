using FastMember;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Utils.GenericMath;
//using System.Windows.Media;

namespace Utils.Extensions
{
    public static class MiscExtensions
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
            if (Tools<T>.LessThan( value, min))
                return min;
            if (Tools<T>.GreaterThan(value, max))
                return max;
            return value;
        }

        //public static T ConvertFromDBVal<T>(this object obj)
        //{
        //    if (obj == null || obj == DBNull.Value)
        //    {
        //        return default(T); // returns the default value for the type
        //    }
        //    else
        //    {
        //        return (T)obj;
        //    }
        //}

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

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



}

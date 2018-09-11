
//Auto Geenerated

namespace Utils
{
	public static class MathExt
	{
#region Clamp
      
		public static byte Clamp(this byte value, byte min, byte max)
		{ return (value < min)? min:((value > max)?max:value); } 

		public static sbyte Clamp(this sbyte value, sbyte min, sbyte max)
		{ return (value < min)? min:((value > max)?max:value); } 

		public static short Clamp(this short value, short min, short max)
		{ return (value < min)? min:((value > max)?max:value); } 

		public static ushort Clamp(this ushort value, ushort min, ushort max)
		{ return (value < min)? min:((value > max)?max:value); } 

		public static int Clamp(this int value, int min, int max)
		{ return (value < min)? min:((value > max)?max:value); } 

		public static uint Clamp(this uint value, uint min, uint max)
		{ return (value < min)? min:((value > max)?max:value); } 

		public static long Clamp(this long value, long min, long max)
		{ return (value < min)? min:((value > max)?max:value); } 

		public static ulong Clamp(this ulong value, ulong min, ulong max)
		{ return (value < min)? min:((value > max)?max:value); } 

		public static float Clamp(this float value, float min, float max)
		{ return (value < min)? min:((value > max)?max:value); } 

		public static double Clamp(this double value, double min, double max)
		{ return (value < min)? min:((value > max)?max:value); } 

		public static decimal Clamp(this decimal value, decimal min, decimal max)
		{ return (value < min)? min:((value > max)?max:value); } 
#endregion
#region GetBigger
		
		public static byte GetBigger(this byte value, byte other)
		{ return (value > other)? value : other; }

		public static sbyte GetBigger(this sbyte value, sbyte other)
		{ return (value > other)? value : other; }

		public static short GetBigger(this short value, short other)
		{ return (value > other)? value : other; }

		public static ushort GetBigger(this ushort value, ushort other)
		{ return (value > other)? value : other; }

		public static int GetBigger(this int value, int other)
		{ return (value > other)? value : other; }

		public static uint GetBigger(this uint value, uint other)
		{ return (value > other)? value : other; }

		public static long GetBigger(this long value, long other)
		{ return (value > other)? value : other; }

		public static ulong GetBigger(this ulong value, ulong other)
		{ return (value > other)? value : other; }

		public static float GetBigger(this float value, float other)
		{ return (value > other)? value : other; }

		public static double GetBigger(this double value, double other)
		{ return (value > other)? value : other; }

		public static decimal GetBigger(this decimal value, decimal other)
		{ return (value > other)? value : other; }
#endregion
#region GetSmaller
		
		public static byte GetSmaller(this byte value, byte other)
		{ return (value > other)? value : other; }

		public static sbyte GetSmaller(this sbyte value, sbyte other)
		{ return (value > other)? value : other; }

		public static short GetSmaller(this short value, short other)
		{ return (value > other)? value : other; }

		public static ushort GetSmaller(this ushort value, ushort other)
		{ return (value > other)? value : other; }

		public static int GetSmaller(this int value, int other)
		{ return (value > other)? value : other; }

		public static uint GetSmaller(this uint value, uint other)
		{ return (value > other)? value : other; }

		public static long GetSmaller(this long value, long other)
		{ return (value > other)? value : other; }

		public static ulong GetSmaller(this ulong value, ulong other)
		{ return (value > other)? value : other; }

		public static float GetSmaller(this float value, float other)
		{ return (value > other)? value : other; }

		public static double GetSmaller(this double value, double other)
		{ return (value > other)? value : other; }

		public static decimal GetSmaller(this decimal value, decimal other)
		{ return (value > other)? value : other; }
#endregion
#region Lerp
      
		public static int Lerp(this int value1, int value2, double percent)
		{ return (int)(value1 * (1 - percent)) + (int)(value2 * percent); } 

		public static uint Lerp(this uint value1, uint value2, double percent)
		{ return (uint)(value1 * (1 - percent)) + (uint)(value2 * percent); } 

		public static long Lerp(this long value1, long value2, double percent)
		{ return (long)(value1 * (1 - percent)) + (long)(value2 * percent); } 

		public static ulong Lerp(this ulong value1, ulong value2, double percent)
		{ return (ulong)(value1 * (1 - percent)) + (ulong)(value2 * percent); } 

		public static float Lerp(this float value1, float value2, double percent)
		{ return (float)(value1 * (1 - percent)) + (float)(value2 * percent); } 

		public static double Lerp(this double value1, double value2, double percent)
		{ return (double)(value1 * (1 - percent)) + (double)(value2 * percent); } 

		public static decimal Lerp(this decimal value1, decimal value2, decimal percent)
		{ return (value1 * (1 - percent)) + (value2 * percent); }
#endregion
#region LerpSafe
      
		public static int LerpSafe(this int value1, int value2, double percent)
		{ 
		 var p = Clamp(percent,0,1);
		return (int)(value1 * (1 - p)) + (int)(value2 * p); 
		} 

		public static uint LerpSafe(this uint value1, uint value2, double percent)
		{ 
		 var p = Clamp(percent,0,1);
		return (uint)(value1 * (1 - p)) + (uint)(value2 * p); 
		} 

		public static long LerpSafe(this long value1, long value2, double percent)
		{ 
		 var p = Clamp(percent,0,1);
		return (long)(value1 * (1 - p)) + (long)(value2 * p); 
		} 

		public static ulong LerpSafe(this ulong value1, ulong value2, double percent)
		{ 
		 var p = Clamp(percent,0,1);
		return (ulong)(value1 * (1 - p)) + (ulong)(value2 * p); 
		} 

		public static float LerpSafe(this float value1, float value2, double percent)
		{ 
		 var p = Clamp(percent,0,1);
		return (float)(value1 * (1 - p)) + (float)(value2 * p); 
		} 

		public static double LerpSafe(this double value1, double value2, double percent)
		{ 
		 var p = Clamp(percent,0,1);
		return (double)(value1 * (1 - p)) + (double)(value2 * p); 
		} 

		public static decimal LerpSafe(this decimal value1, decimal value2, decimal percent)
		{
			var p = Clamp(percent,0,1);
			return (value1 * (1 - p)) + (value2 * p); 
		}
#endregion
#region InterpBy
      
		public static int InterpBy(this int value, int goal, int amount)
		{ 
			return LerpSafe(value, goal, (goal-value)/System.Math.Abs(amount));
		} 

		public static uint InterpBy(this uint value, uint goal, uint amount)
		{ 
			return LerpSafe(value, goal, (goal-value)/System.Math.Abs(amount));
		} 

		public static long InterpBy(this long value, long goal, long amount)
		{ 
			return LerpSafe(value, goal, (goal-value)/System.Math.Abs(amount));
		} 

		public static float InterpBy(this float value, float goal, float amount)
		{ 
			return LerpSafe(value, goal, (goal-value)/System.Math.Abs(amount));
		} 

		public static double InterpBy(this double value, double goal, double amount)
		{ 
			return LerpSafe(value, goal, (goal-value)/System.Math.Abs(amount));
		} 

		public static decimal InterpBy(this decimal value, decimal goal, decimal amount)
		{ 
			return LerpSafe(value, goal, (goal-value)/System.Math.Abs(amount));
		} 

		public static ulong InterpBy(this ulong value, ulong goal, ulong amount)
		{ 
			return LerpSafe(value, goal, (goal-value)/amount);
		} 
#endregion
	}
}

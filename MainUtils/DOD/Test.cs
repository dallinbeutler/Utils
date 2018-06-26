using System;
using System.Collections.Generic;
using System.Text;
using Utils.DOD;
using System.Reactive;
using System.Reactive.Linq;
using System.Diagnostics;
using System.Timers;

namespace UtilsMain.DOD
{
    struct Vec2
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public static Vec2 operator + (Vec2 lhs , Vec2 other)
        {
            return new Vec2(lhs.X + other.X, lhs.Y + other.Y);
        }
        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }
    }
    public class MainLoop
    {
        DataStream<Vec2> Position = new DataStream<Vec2>();
        DataStream<Vec2> Velocity = new DataStream<Vec2>();
        DataStream<Vec2> Dimensions = new DataStream<Vec2>();
        DataStream<ConsoleColor> color= new DataStream<ConsoleColor>();

        public MainLoop()
        {
            Position[9] = new Vec2(0, 0);
            Velocity[9] = new Vec2(1,1);
            Dimensions[9] = new Vec2(1,1);
            color[9] = ConsoleColor.Red; 

            
            Position.ToObservable().Subscribe((x)=> 
            {
                using (Utils.ConsoleColor cc = new Utils.ConsoleColor(color[x.Entity]))
                {
                    Console.WriteLine("Position Change! "+ x.Entity + " - " + x.OldVal + " => " + x.NewVal);

                }
            });
        }

        public void Update()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            while (cki.Key != ConsoleKey.Escape)
            {
                //if (stopwatch.ElapsedMilliseconds)
                //{
                Position[9] += new Vec2(2, 2);
                //}
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Spacebar)
                {
                    color[9] = ConsoleColor.Green;
                }
            }

        }
        public static class Program
        {
            public static void Main()
            {
                MainLoop loop = new MainLoop();
                loop.Update();
            }
        }
    }
}

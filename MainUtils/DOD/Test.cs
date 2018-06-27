using System;
using System.Collections.Generic;
using System.Text;
using Utils.DOD;
using System.Reactive;
using System.Reactive.Linq;
using System.Diagnostics;
using System.Timers;
using System.Threading;
using Utils.Extensions;
using System.Linq;

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
        DataStream<string> fools = new DataStream<string>();
        public MainLoop()
        {
            Position[9] = new Vec2(0, 0);
            Velocity[9] = new Vec2(1,1);
            Dimensions[9] = new Vec2(1,1);
            color[9] = ConsoleColor.Red;
            var OPos = Position.ToObservable();


            OPos.Subscribe((x)=> 
            {
                using (Utils.ConsoleColor cc = new Utils.ConsoleColor(color[x.Entity]))
                {
                    Console.WriteLine("Position Change! "+ x.Entity + " - " + x.OldVal + " => " + x.NewVal);

                }
            });
            Observable.Interval(new TimeSpan(0,0,1)).Subscribe((long ticks)=> { Position[9] += new Vec2(-1, -1); });

            var acc = FastMember.TypeAccessor.Create(typeof(MainLoop));
            var mem = acc.GetMembers();
        }

        public void Update()
        {
            Console.WriteLine("start loop");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            while (cki.Key != ConsoleKey.Escape)
            {

                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Spacebar)
                {
                    var colvals = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>();
                    color[9] = colvals.Random();
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

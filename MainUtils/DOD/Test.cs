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
using System.Reflection;

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
      public static Vec2 operator +(Vec2 lhs, Vec2 other)
      {
         return new Vec2(lhs.X + other.X, lhs.Y + other.Y);
      }
      public static Vec2 operator -(Vec2 lhs, Vec2 other)
      {
         return new Vec2(lhs.X - other.X, lhs.Y - other.Y);
      }
      public override string ToString()
      {
         return "(" + X + "," + Y + ")";
      }
   }
   struct Parent : IDisposable
   {
      IDisposable Subscription;
      [Flags]
      public enum LockAxis
      {
         x   = 0x000001,
         y   = 0x000010,
         z   = 0x000100,
         rx  = 0x001000,
         ry  = 0x010000,
         rz  = 0x100000,
         all = 0x111111,
         pos = 0x000111,
         rot = 0x111000
      }
      //not actually using LockAxis, but this could be how to specify what you want to adjust on
      public Parent(int parent, int child, LockAxis lockon,  IObservable<DSChangedArgs<Vec2>> position, DataStream<Vec2> posDS)
      {       
         Subscription = position.Where(x=>x.Entity == parent).Subscribe((x) => 
         {
            posDS[child] += (x.NewVal - x.OldVal);
         });
      }
      public void Dispose()
      {
         Subscription.Dispose();
      }
   }

   public class MainLoop
   {
      //initiallize our Data collections.
      DataStream<Vec2> Position = new DataStream<Vec2>();
      DataStream<ConsoleColor> color = new DataStream<ConsoleColor>();
      DataStream<Parent> DParent = new DataStream<Parent>();

      //not used yet
      //DataStream<Vec2> Velocity = new DataStream<Vec2>();
      //DataStream<Vec2> Dimensions = new DataStream<Vec2>();
      //DataStream<string> fools = new DataStream<string>();

      // just a unique set of ints representing our entities
      HashSet<int> Entities = new HashSet<int>();
      public MainLoop()
      {
         //Just marking in entities list that we have entities with these int IDs 
         Entities.Add(3);
         Entities.Add(9);

         //giving entity 9 a position component
         Position[9] = new Vec2(0, 0);
         //giving entity 3 a position component
         Position[3] = new Vec2(6, 6);

         //not used yet
         //Velocity[9] = new Vec2(1, 1);
         //Dimensions[9] = new Vec2(1, 1);

         //giving entity 3 and 9 colors
         color[9] = ConsoleColor.Red;
         color[3] = ConsoleColor.Cyan;
     
         //converting our positions stream to an observable
         var OPos = Position.ToObservable();

         // making 3 a child of 9's position
         DParent[3] = new Parent(9,3,Parent.LockAxis.all, OPos,Position);

         // a subscription to tell us when an entity's position has changed;
         OPos.Subscribe((x) =>
         {
            using (Utils.ConsoleColor cc = new Utils.ConsoleColor(color[x.Entity]))
            {
               Console.WriteLine("Position Change! " + x.Entity + " - " + x.OldVal + " => " + x.NewVal);

            }
         });

         // a subscription to subtract entity 9's position every second
         Observable.Interval(new TimeSpan(0, 0, 1)).Subscribe((long ticks) => { Position[9] += new Vec2(-1, -1); });        
      }
      public void Update()
      {
         Console.WriteLine("start loop");

         ConsoleKeyInfo cki = new ConsoleKeyInfo();
         
         // main loop
         while (cki.Key != ConsoleKey.Escape)
         {

            // change the color of 9 if we press space
            cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Spacebar)
            {
               var colvals = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>();
               color[9] = colvals.Random();
            }
         }
      }
   }
}

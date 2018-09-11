using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using DOD;
using OpenTK;
using Reactive.Bindings;
using Utils;
using Utils.Extensions;
using UtilsNET;


namespace Utils
{// possible source for inspiration:https://www.learnrxjs.io/recipes/gameloop.html
   public class IE
   {
      static void Compose(string DS, Type type, dynamic initVal)
      {
         if (!Things.ContainsKey(DS))
         {

         }
         //Things.Add(DS,)
      }
      static Dictionary<string, DataStream<long,dynamic>> Things = new Dictionary<string, DataStream<long,dynamic>>();
      
      
   }
   public class MainLoop
   {
      

      //initiallize our Data collections.
      DataStream<long,Matrix4> DTransform = new DataStream<long,Matrix4>("Transforms");
      DataStream<long,System.ConsoleColor> DColor = new DataStream<long,System.ConsoleColor>("ConsoleColors");
      DataStream<long, Parent> DParent = new DataStream<long, Parent>("Parent");
      //not used yet

      // just a unique set of ints representing our entities
      HashSet<int> Entities = new HashSet<int>();
      //IDisposable diltime;
      public MainLoop()
      {
         //diltime = Observable.Create();
         //Just marking in entities list that we have entities with these int IDs 
         Entities.Add(3);
         Entities.Add(9);
         //converting our positions stream to an observable
         //var OPos = DTransform.ToObservable();

         //giving entity 9 a position component
         DTransform[9] = Matrix4.CreateTranslation(0, 1, 1);
         //giving entity 3 a position component
         DTransform[3] = Matrix4.CreateTranslation(0, 6, 3);

         
         //not used yet
         //Velocity[9] = new Vec2(1, 1);
         //Dimensions[9] = new Vec2(1, 1);

         //giving entity 3 and 9 colors
         DColor[9] = System.ConsoleColor.Red;
         DColor[3] = System.ConsoleColor.Cyan;


         // making 3 a child of 9's position
         DParent[3] = new Parent(9, 3, Parent.LockAxis.all, DTransform);

         // a subscription to tell us when an entity's position has changed;
         DTransform.AsObservableDetails.Subscribe((x) =>
         {
            using (ConsoleColors cc = new Utils.ConsoleColors(DColor[x.Entity]))
            {
               Console.WriteLine("Position Change! " + x.Entity + " - " + x.OldVal.ExtractTranslation() + " => " + x.NewVal.ExtractTranslation());
            }
         });
         // a subscription to subtract entity 9's position every second
         Observable.Interval(new TimeSpan(0, 0, 1)).Subscribe((long ticks) => { DTransform[9] += Matrix4.CreateTranslation(-1, -1, 0); });
      }
      public void Update()
      {
         Console.WriteLine("start loop");

         ConsoleKeyInfo cki = new ConsoleKeyInfo();
         MyMath.Easing.LinearEase backEaseIn = new MyMath.Easing.LinearEase(0, 0, 0);

         //var easer = new MyMath.Easing.LinearEase(0,1,3).Eval(3);
         //Entity e = new Entity()

         //Stopwatch elapsed = new Stopwatch();
         //elapsed.Start();
         Stopwatch stopwatch = new Stopwatch();
         stopwatch.Start();
         Observable.Interval(new TimeSpan(), Scheduler.Default);
         
         // main loop
         while (cki.Key != ConsoleKey.Escape)
         {
            float delta = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();
            
            // change the color of 9 if we press space
            cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Spacebar)
            {
               var colvals = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>();
               DColor[9] = colvals.Random();
            }
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

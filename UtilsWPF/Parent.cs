using DOD;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsNET
{
   struct Parent : IDisposable
   {
      IDisposable Subscription;
      [Flags]
      public enum LockAxis
      {
         Location,
         Rotation,
         Scale,
         all
      }

      //not actually using LockAxis, but this could be how to specify what you want to adjust on
      public Parent(int parent, int child, LockAxis lockon, IObservable<DSChangedArgs<Matrix4>> position, DataStream<Matrix4> posDS)
      {
         if (lockon == LockAxis.all)
         {
            Subscription = position.Where(x => x.Entity == parent).Subscribe((x) =>
            {
               posDS[child] += (x.NewVal - x.OldVal);
            });
            return;
         }
         if (lockon == LockAxis.Location)
         {
            Subscription = position.Where(x => x.Entity == parent).Subscribe((x) =>
            {
               posDS[child] += (x.NewVal - x.OldVal).ClearRotation().ClearScale();
            });
            return;
         }
         if (lockon == LockAxis.Rotation)
         {
            Subscription = position.Where(x => x.Entity == parent).Subscribe((x) =>
            {
               posDS[child] += (x.NewVal - x.OldVal).ClearTranslation().ClearScale();
            });
            return;
         }
         if (lockon == LockAxis.Scale)
         {
            Subscription = position.Where(x => x.Entity == parent).Subscribe((x) =>
            {
               posDS[child] += (x.NewVal - x.OldVal).ClearTranslation().ClearRotation();
            });
            return;
         }
         else
         {
            throw new Exception("lockAxis not implemented!");
         }
      }
      public void Dispose()
      {
         Subscription.Dispose();
      }
   }
}

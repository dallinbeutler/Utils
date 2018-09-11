using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace DOD
{
      public interface INotify<T> { event NotifyChangedEventHandler<T> Notify; }
      public delegate void NotifyChangedEventHandler<T>(T oldVal, T newVal);
   public struct OProp<T> :  INotify<T>, IEquatable<T>
   {
      public event NotifyChangedEventHandler<T> Notify;
      T Value;
      public override string ToString()
      {
         return Value.ToString();
      }
      public T Get()
      {
         return Value;
      }
      public bool Set(T inVal)
      {
         if (Value.Equals(inVal))
         {
            return false;
         }
         else
         {
            Notify.Invoke(Value, inVal);
            Value = inVal;
            return true;
         }
      }
      public bool Set(OProp<T> inVal)
      {
         if (Value.Equals(inVal.Value))
         {
            return false;
         }
         else
         {
            Notify.Invoke(Value, inVal.Value);

            Value = inVal.Value;
            return true;
         }
      }
      public IObservable<NotifyChangedEventHandler<T>> ToObservable()
      {
         var foo = Observable
      .FromEventPattern<NotifyChangedEventHandler<T>>(this, "Notify")
      .Select(change => change.EventArgs);
         return foo;
      }
      public bool Equals(T other)
      {
         return Value.Equals(other);
      }
   }


}

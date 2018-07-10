using DOD;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace UtilsNET
{
   public struct Dil
   {
      public bool GlobalAffects;
      public float IndividualAmount;

   }
   public class DilationSystem
   {
      public DataStream<Dil> stream;

      public float GlobalDilation = 1.0f;
      public DilationSystem()
      {
         stream = new DataStream<Dil>();
         //We could treat these like Global entities not affected by Entity Dilation, and is [not] affected by the global dilation
         stream[-1] = new Dil() { GlobalAffects = true, IndividualAmount = 1.0f };
         stream[-2] = new Dil() { GlobalAffects = false, IndividualAmount = 1.0f };
      }
      public float getDilation(int entity)
      {
         var e = stream[entity];
         return e.GlobalAffects ? (GlobalDilation * e.IndividualAmount) : e.IndividualAmount;
      }

      public List<TimedEvent> timedEvents = new List<TimedEvent>();
      public void Update(long delta)
      {
         foreach (var i in timedEvents)
         {
               i.Update((long)(getDilation(i.Entity) * delta));
         }
      }
   }
   public enum TimerState
   {
      Paused,
      Completed,
      Restarted,
      Resumed,
   }
   public interface ITimer : IDisposable
   {
      long MSStart { get; }
      long MSLeft { get; }
      Subject<TimerState> OnCompleted { get; }
      bool IsPaused { get; }
      void Pause();
      void Resume();
      void Toggle();
      void Restart();
   }

   public struct TimedEvent : ITimer
   {
      public long MSStart { get; private set; }
      public long MSLeft { get; private set; }
      public Subject<TimerState> OnCompleted { get; private set; }
      public bool IsPaused { get; private set; }
      public int Entity { get; private set; }
      public TimedEvent(int Entity, TimeSpan timeSpan)
      {
         MSLeft = timeSpan.Milliseconds;
         MSStart = MSLeft;
         OnCompleted = new Subject<TimerState>();
         IsPaused = false;
         this.Entity = Entity;
      }

      public void Dispose()
      {
         OnCompleted.Dispose();
      }
      public void Pause()
      {
         if (IsPaused == false)
         {
            IsPaused = true;
            OnCompleted.OnNext(TimerState.Paused);
         }
      }
      public void Resume()
      {
         if (IsPaused == true)
         {
            IsPaused = false;
            OnCompleted.OnNext(TimerState.Paused);
         }
      }
      public void Toggle()
      {
         if (IsPaused == true)
         {
            IsPaused = false;
            OnCompleted.OnNext(TimerState.Paused);
         }
         else
         {
            IsPaused = true;
            OnCompleted.OnNext(TimerState.Resumed);
         }
      }
      public void Restart()
      {
         MSLeft = MSStart;
         OnCompleted.OnNext(TimerState.Restarted);
      }
      public void Update(long delta)
      {
         if (MSLeft > 0)
         {
            MSLeft -= delta;
            if (MSLeft <= 0)
            {
               OnCompleted.OnNext(TimerState.Completed);
            }
         }
      }
      //public enum TimerState
      //{
      //   Paused,
      //   Completed,
      //   Restarted,
      //   Resumed,
      //}
   }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.DOD
{
    public interface INotifyCollectionChanged<T> {  event NotifyDataStreamChangedEventHandler<T> DataStreamChanged; }
    public delegate void NotifyDataStreamChangedEventHandler<T>(object sender, DSChangedArgs<T> args);
    public struct DSChangedArgs<T>
    {
        public int Entity;
        public NotifyCollectionChangedAction Action;
        public T OldVal;
        public T NewVal;

        public DSChangedArgs(int entity, NotifyCollectionChangedAction action, T oldVal, T newVal){
            Entity = entity;
            Action = action;
            OldVal = oldVal;
            NewVal = newVal;
        }
    }
    // Should I do my own Collection change/ datapool enumeration?
    //public enum EventChange
    //{

    //}

    // Can't store strings with struct restriction...
    public class DataStream<T> : INotifyCollectionChanged<T> //where T //: struct
    {
        ConcurrentDictionary<int, T> Set;
        public event NotifyDataStreamChangedEventHandler<T> DataStreamChanged;

        public DataStream()
        {
            Set = new ConcurrentDictionary<int, T>();
            DataStreamChanged = new NotifyDataStreamChangedEventHandler<T>((x,y) => { return; });
        }

        public int Count
        {
            get
            {
                return Set.Count;
            }
        }

        public T this[int i]
        {
            get
            {
                return Set[i];
            }
            set
            {
                if (!Set.ContainsKey(i))
                {
                    Set[i] = value;
                    DataStreamChanged.Invoke(this, new DSChangedArgs<T>(i, NotifyCollectionChangedAction.Add, default(T), value));//this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<int, T>(i, value)));
                }
                else if (!EqualityComparer<T>.Default.Equals(Set[i], value))
                {
                    Set[i] = value;
                    DataStreamChanged.Invoke(this, new DSChangedArgs<T>(i, NotifyCollectionChangedAction.Replace, default(T), value));//(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, new KeyValuePair<int, T>(i, value)));
                }
            }
        }
        public T RemoveAt(int i)
        {
            T val;
            if (Set.TryRemove(i, out val))
            {
                DataStreamChanged.Invoke(this, new DSChangedArgs<T>(i, NotifyCollectionChangedAction.Remove,val,default(T)));//this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<int, T>(i, val)));
                return val;
            }
            return default(T);
        }
        public void Clear()
        {
            Set.Clear(); 
            DataStreamChanged.Invoke(this, new DSChangedArgs<T>(-1, NotifyCollectionChangedAction.Reset, default(T), default(T))); //this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public IObservable<DSChangedArgs<T>> ToObservable()
        {
            var foo = Observable
         .FromEventPattern<DSChangedArgs<T>>(this, "DataStreamChanged")
         .Select(change => change.EventArgs);
            return foo;
        }

        public void Merge()
        {
            ToObservable().Merge(ToObservable());
        }
    }
}

﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;

namespace DOD
{


   //public interface INotifyCollectionChanged<T> { event NotifyDataStreamChangedEventHandler<T> DataStreamChanged; }

   //public delegate void NotifyEntityAddedRemovedEventHandler(IDataStream sender, EntityChangedArgs args);
   public delegate void NotifyDataStreamChangedEventHandler<Key>(IDataStream<Key> sender, EntityChangedArgs<Key> args);
   public delegate void NotifyDataStreamChangedEventHandler<Key, T>(IDataStream<Key> sender, DSChangedArgs<Key, T> args);
   public class DSChangedArgs<Key,T> : EntityChangedArgs<Key>
   {
      public T OldVal;
      public T NewVal;
      public DSChangedArgs(Key entity, NotifyCollectionChangedAction action, T oldVal, T newVal):base(entity,action)
      {
         OldVal = oldVal;
         NewVal = newVal;
      }
   }
   public class EntityChangedArgs<Key>
   {
      public Key Entity;
      public NotifyCollectionChangedAction Action;
      public EntityChangedArgs(Key entity, NotifyCollectionChangedAction action)
      {
         Entity = entity;
         Action = action;
      }
   }

   public interface IDataStream<Key> 
   {
      string Name { get; }
      int Count { get; }
       bool HasEntity(Key ID);
       void Clear();
       bool RemoveAt(Key ID);
       void Set(Key ID, object o);
       object Get(Key ID);
       event NotifyDataStreamChangedEventHandler<Key> DataStreamChanged;

   }

   /// <summary>
   /// This class is basically for storing values in a way that is easier on memory, 
   /// while also generating events on property change that can be observed.
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public class DataStream<Key,T> : IDataStream<Key>, IEnumerable,  IEnumerable<KeyValuePair<Key, T>> //INotifyCollectionChanged<T>,
   {
      private ConcurrentDictionary<Key, T> DataSet;
      public event NotifyDataStreamChangedEventHandler<Key,T> EntityChanged;
      public event NotifyDataStreamChangedEventHandler<Key> DataStreamChanged;

      //public event NotifyEntityAddedRemovedEventHandler EntityChanged;
      //public event NotifyEntityAddedRemovedEventHandler DataStreamChanged;

      public IObservable<EntityChangedArgs<Key>> AsObservable { get;}
      public IObservable<DSChangedArgs<Key,T>> AsObservableDetails { get;}
      public string Name { get; }

      public DataStream(string Name, IEnumerable<KeyValuePair<Key,T>> comps = null)
      {
         this.Name = Name;

         this.EntityChanged += DataStream_EntityChanged;
         this.DataStreamChanged += DataStream_DataStreamChanged;
         if (comps != null)
            DataSet = new ConcurrentDictionary<Key, T>(comps);
         else
            DataSet = new ConcurrentDictionary<Key, T>();
         //DataStreamChanged = new NotifyDataStreamChangedEventHandler<T>((x, y) => { return; });
         AsObservable = Observable
         .FromEventPattern<EntityChangedArgs<Key>>(this, "DataStreamChanged")
         .Select(change => change.EventArgs);

         AsObservableDetails = Observable
         .FromEventPattern<DSChangedArgs<Key, T>>(this, "EntityChanged")
         .Select(change => change.EventArgs);
      }

      private void DataStream_DataStreamChanged(IDataStream<Key> sender, EntityChangedArgs<Key> args)
      {
      }

      private void DataStream_EntityChanged(IDataStream<Key> sender, DSChangedArgs<Key, T> args)
      {
         DataStreamChanged.Invoke(this, args);
      }

      

      public int Count
      {
         get
         {
            return DataSet.Count;
         }
      }

      public bool HasEntity(Key ID)
      {
         return DataSet.Keys.Contains(ID);
      }
      public T this[Key i]
      {
         get
         {
            return DataSet[i];
         }
         set
         {
            if (!DataSet.ContainsKey(i))
            {
               DataSet[i] = value;
               EntityChanged.Invoke(this, new DSChangedArgs<Key,T>(i, NotifyCollectionChangedAction.Add, default(T), value));
            }
            else if (!EqualityComparer<T>.Default.Equals(DataSet[i], value))
            {
               DataSet[i] = value;
               EntityChanged.Invoke(this, new DSChangedArgs<Key,T>(i, NotifyCollectionChangedAction.Replace, default(T), value));//(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, new KeyValuePair<int, T>(i, value)));
            }
         }
      }
      public bool RemoveAt(Key i)
      {
         T val;
         if (DataSet.TryRemove(i, out val))
         {
            EntityChanged.Invoke(this, new DSChangedArgs<Key,T>(i, NotifyCollectionChangedAction.Remove, val, default(T)));//this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<int, T>(i, val)));
            return true;
         }
         return false;
      }
      public T PopAt(Key i)
      {
         T val;
         if (DataSet.TryRemove(i, out val))
         {
            EntityChanged.Invoke(this, new DSChangedArgs<Key,T>(i, NotifyCollectionChangedAction.Remove, val, default(T)));//this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<int, T>(i, val)));
            return val;
         }
         return default(T);
      }
      public void Clear()
      {
         DataSet.Clear();
         EntityChanged.Invoke(this, new DSChangedArgs<Key,T>(default(Key), NotifyCollectionChangedAction.Reset, default(T), default(T))); //this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      }


      public  IEnumerator<KeyValuePair<Key, T>> GetEnumerator()
      {
         return DataSet.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return DataSet.GetEnumerator();
      }

      public void Set(Key ID, object o)
      {
         if (o is T t)
          this[ID] = t;
         else
         {
            Console.WriteLine("Error converting "+ o.GetType() + " to " + typeof(T));
         }
      }

      public object Get(Key ID)
      {
         return this[ID];
      }

   }
}

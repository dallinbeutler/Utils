using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Linq;
using System.ComponentModel.Composition;

namespace DOD
{
   class DSManager
   {
      private long CurID;
      //[ImportMany(typeof(IDataStream<long>))]
      DataStream<string, IDataStream<long>> ComponentSystems;//   = new DataStream<string, IDataStream<long>>("CompSystems");
      DataStream<long, List<IDataStream<long>>> Entities = new DataStream<long, List<IDataStream<long>>>("Entities");

      [ImportingConstructor]
      public DSManager(IDataStream<long> systems)
      {
         ComponentSystems = new DataStream<string, IDataStream<long>>("CompSystems");
         ComponentSystems.DataStreamChanged += ComponentSystems_DataStreamChanged;
      }
      long getUniqueID()
      {
         return Interlocked.Increment(ref CurID);
      }

      private void ComponentSystems_DataStreamChanged(IDataStream<string> sender, EntityChangedArgs<string> args)// DSChangedArgs<string, IDataStream<long>> args)
      {
         var myargs = args as DSChangedArgs<string, IDataStream<long>>;
         if (myargs.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
         {
            myargs.NewVal.DataStreamChanged += ComponentChange;
         }
         else if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
         {
            myargs.NewVal.DataStreamChanged -= ComponentChange;
         }
      }

      private void ComponentChange(IDataStream<long> sender, EntityChangedArgs<long> args)
      {
         if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
         {
            Entities[args.Entity].Add(sender);
         }
         if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
         {
            Entities[args.Entity].Remove(sender);
         }
      }

      public void AddEntity(params CompPair[] compPairs)
      {
         long ID = getUniqueID();

         foreach(CompPair p in compPairs)
         {
            ComponentSystems[p.system.Name].Set(ID,p.Value);
         }
      }
      public void KillEntity(long ID)
      {
         Entities[ID].ForEach(x => x.RemoveAt(ID));
      }

      public struct CompPair 
      {
         //public string DSName { get; }
         public IDataStream<long> system { get;}
         public object Value { get; }
         public CompPair(  IDataStream<long> System, object value)
         {
            //this.DSName = name;
            this.system = System;
            this.Value = value;

            //Do we need to add to components list?
         }
      }
      
      //private void AllComponents_DataStreamChanged(object sender, DSChangedArgs<IDataStream<long>> args)
      //{

      //   if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
      //   {
      //      args.NewVal.DataStreamChanged += NewVal_DataStreamChanged;
      //   }
      //   else if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
      //   {

      //   }


      //}

      //private void NewVal_DataStreamChanged(IDataStream sender, EntityChangedArgs args)
      //{
      //   throw new NotImplementedException();
      //}
   }



   //////public class Entity
   //////{
   //////   public long ID { get; }
   //////   public IEnumerable<IDataStream> Components;
   //////   public Entity(long id, params IDSValuePair[] components)
   //////   {
   //////      this.ID = id;
   //////      foreach (var c in components)
   //////      {
   //////         c.DS.Set(ID, c.Value);
   //////      }
   //////   }
   //////   public interface IDSValuePair
   //////   {
   //////      IDataStream DS { get; }
   //////      object Value { get; }
   //////   }
   //////   public struct DSValuePair<T> : IDSValuePair
   //////   {
   //////      public DataStream<T> DS { get; }
   //////      IDataStream IDSValuePair.DS { get => DS; }
   //////      public T Value { get; }
   //////      object IDSValuePair.Value { get => Value; }

   //////      public DSValuePair(DataStream<T> DS, T Value)
   //////      {
   //////         this.DS = DS;
   //////         this.Value = Value;
   //////      }
   //////   }
   //////}

   //public interface IEntity
   //{

   //   int ID { get; set; }

   //   int Components { get; set; }
   //}
}

//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.ComponentModel;
//using System.Linq;
//using System.Reactive.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DallinUtils
//{
//    public interface INotifyCollectionChanged<T> {  event NotifyDataStreamChangedEventHandler<T> DataStreamChanged; }

//    public delegate void NotifyDataStreamChangedEventHandler<T>(object sender, NotifyDataStreamChangedEventArgs<T> args);

//    public struct NotifyDataStreamChangedEventArgs<T>
//    {
//        public int Entity;
//        public NotifyCollectionChangedAction Action;
//        public T OldVal;
//        public T NewVal;

//        public NotifyDataStreamChangedEventArgs(int entity, NotifyCollectionChangedAction action, T oldVal, T newVal){
//            Entity = entity;
//            Action = action;
//            OldVal = oldVal;
//            NewVal = newVal;
//        }
//    }
//    // Should I do my own Collection change/ datapool enumeration?
//    //public enum EventChange
//    //{

//    //}
//    public class DataStream<T> : INotifyCollectionChanged<T> where T: struct
//    {
//        ConcurrentDictionary<int, T> Set;
//        public event NotifyDataStreamChangedEventHandler<T> DataStreamChanged;
//        public int Count
//        {
//            get
//            {
//                return Set.Count;
//            }
//        }

//        public T this[int i]
//        {
//            get
//            {
//                return Set[i];
//            }
//            set
//            {
//                if (!Set.ContainsKey(i))
//                {
//                    Set[i] = value;
//                    DataStreamChanged.Invoke(this, new NotifyDataStreamChangedEventArgs<T>(i, NotifyCollectionChangedAction.Add, default(T), value));//this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<int, T>(i, value)));
//                }
//                else if (!EqualityComparer<T>.Default.Equals(Set[i], value))
//                {
//                    Set[i] = value;
//                    DataStreamChanged.Invoke(this, new NotifyDataStreamChangedEventArgs<T>(i, NotifyCollectionChangedAction.Replace, default(T), value));//(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, new KeyValuePair<int, T>(i, value)));
//                }
//            }
//        }
//        public T RemoveAt(int i)
//        {
//            T val;
//            if (Set.TryRemove(i, out val))
//            {
//                DataStreamChanged.Invoke(this, new NotifyDataStreamChangedEventArgs<T>(i, NotifyCollectionChangedAction.Remove,val,default(T)));//this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<int, T>(i, val)));
//                return val;
//            }
//            return default(T);
//        }
//        public void Clear()
//        {
//            Set.Clear(); 
//            DataStreamChanged.Invoke(this, new NotifyDataStreamChangedEventArgs<T>(-1, NotifyCollectionChangedAction.Reset, default(T), default(T))); //this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
//        }

//        public IObservable<NotifyDataStreamChangedEventArgs<T>> ToObservable()
//        {
//            var foo = Observable
//         .FromEventPattern<NotifyDataStreamChangedEventArgs<T>>(this, "CollectionChanged")
//         .Select(change => change.EventArgs);
//            return foo;
//        }

//        public void Merge()
//        {
//            ToObservable().Merge(ToObservable());
//        }
//    }



//    ////public class ObjectPool<T> : INotifyCollectionChanged, INotifyPropertyChanged where T : struct
//    ////{
//    ////    private ConcurrentBag<T> _objects;
//    ////    private Func<T> _objectGenerator;

//    ////    public event NotifyCollectionChangedEventHandler CollectionChanged;
//    ////    public event PropertyChangedEventHandler PropertyChanged;

//    ////    public ObjectPool(Func<T> objectGenerator)
//    ////    {
//    ////        if (objectGenerator == null) throw new ArgumentNullException("objectGenerator");
//    ////        _objects = new ConcurrentBag<T>();
//    ////        _objectGenerator = objectGenerator;
//    ////    }

//    ////    public T GetObject()
//    ////    {
//    ////        T item;
//    ////        if (_objects.TryTake(out item)) return item;
//    ////        return _objectGenerator();
//    ////    }

//    ////    public void PutObject(T item)
//    ////    {
//    ////        _objects.Add(item);
//    ////        CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
//    ////    }
//    ////}

//    ////struct BagItem<T> : INotifyCollectionChanged, IDisposable
//    ////{
//    ////    public event NotifyCollectionChangedEventHandler CollectionChanged;

//    ////    private T _Value;
//    ////    public T Value
//    ////    {
//    ////        get
//    ////        {
//    ////            return _Value;
//    ////        }
//    ////        set
//    ////        {
//    ////            // operator== is undefined for generic T; EqualityComparer solves this
//    ////            if (!EqualityComparer<T>.Default.Equals(_Value, value))
//    ////            {
//    ////                _Value = value;
//    ////            }
//    ////        }
//    ////    }

//    ////    public void Dispose()
//    ////    {
//    ////        throw new NotImplementedException();
//    ////    }
//    ////}

//    //    class Program
//    //    {
//    //        static void Main(string[] args)
//    //        {
//    //            CancellationTokenSource cts = new CancellationTokenSource();

//    //            // Create an opportunity for the user to cancel.
//    //            Task.Run(() =>
//    //            {
//    //                if (Console.ReadKey().KeyChar == 'c' || Console.ReadKey().KeyChar == 'C')
//    //                    cts.Cancel();
//    //            });

//    //            ObjectPool<Vec2> pool = new ObjectPool<Vec2>(() => new Vec2());

//    //            // Create a high demand for MyClass objects.
//    //            Parallel.For(0, 1000000, (i, loopState) =>
//    //            {
//    //                Vec2 mc = pool.GetObject();
//    //                Console.CursorLeft = 0;
//    //                // This is the bottleneck in our application. All threads in this loop
//    //                // must serialize their access to the static Console class.
//    //                Console.WriteLine("{0:####.####}", mc.GetValue(i));

//    //                pool.PutObject(mc);
//    //                if (cts.Token.IsCancellationRequested)
//    //                    loopState.Stop();

//    //            });
//    //            Console.WriteLine("Press the Enter key to exit.");
//    //            Console.ReadLine();
//    //            cts.Dispose();
//    //        }

//    //    }

//    //    public struct Phys
//    //    {
//    //        //IObservable<int> obs =
//    //        System.Reactive.Subjects.Subject<int> foo;// = new System.Reactive.Subjects.Subject<int>();
//    //        IObservable<int> bar;

//    //        void fun()
//    //        {
//    //            //var f = new IObservable<int>() { subs};
//    //        }
//    //    }
//    //    //public class Obs : System.IObservable<int>
//    //    //{
//    //    //    public IDisposable Subscribe(IObserver<int> observer)
//    //    //    {
//    //    //        throw new NotImplementedException();
//    //    //    }

//    //    //}

//    //    public struct Vec2
//    //    {
//    //        public float x { get; set; }
//    //        public float y { get; set; }
//    //    }

//    //    // A toy class that requires some resources to create.
//    //    // You can experiment here to measure the performance of the
//    //    // object pool vs. ordinary instantiation.
//    //    //struct MyClass
//    //    //{
//    //    //    public int[] Nums { get; set; }
//    //    //    public double GetValue(long i)
//    //    //    {
//    //    //        return Math.Sqrt(Nums[i]);
//    //    //    }
//    //    //    public MyClass()
//    //    //    {
//    //    //        Nums = new int[1000000];
//    //    //        Random rand = new Random();
//    //    //        for (int i = 0; i < Nums.Length; i++)
//    //    //            Nums[i] = rand.Next();
//    //    //    }
//    //    //}
//    //}


//    ////using System;
//    ////using System.Collections.Generic;
//    ////using System.Linq;
//    ////using System.Text;
//    ////using System.Threading;
//    ////using System.Threading.Tasks;

//    ////namespace DallinUtils
//    ////{

//    ////    public interface IFirst { void FirstMethod(); }
//    ////    public interface ISecond { void SecondMethod(); }

//    ////    public class First : IFirst
//    ////    {
//    ////        public void FirstMethod() { Console.WriteLine("First"); }
//    ////    }

//    ////    public class Second : ISecond
//    ////    {
//    ////        public void SecondMethod() { Console.WriteLine("Second"); }
//    ////    }

//    ////    public class FirstAndSecond : IFirst, ISecond
//    ////    {
//    ////        First first = new First();
//    ////        Second second = new Second();
//    ////        public void FirstMethod() { first.FirstMethod(); }
//    ////        public void SecondMethod() { second.SecondMethod(); }
//    ////    }
//    ////    //reference to parent
//    ////    //public class ParentSystem<T>
//    ////    //{
//    ////    //    Dictionary<int, Vec2> Components = new Dictionary<int, Vec2>();
//    ////    //    void add(int id, int parentID)
//    ////    //    {
//    ////    //        Components.Add(id, PosSystem.Components[parentID]);
//    ////    //    }
//    ////    //    void UpdatePos()
//    ////    //    {
//    ////    //    }
//    ////    //}
//    ////    public static class MainTest{
//    ////    public static void Main()
//    ////    {
//    ////        SystemHandler.positions.Set(0, new Vec2(1, 0));
//    ////        SystemHandler.positions.Set(1, new Vec2(3, 0));
//    ////        SystemHandler.physics.Set(1, new Vec2(3, 1));
//    ////            while (true)
//    ////            {
//    ////                SystemHandler.UpdateAll();
//    ////                //Thread.Sleep(30);
//    ////            }
//    ////    }

//    ////    }
//    ////    public class Vec2
//    ////    {
//    ////        public int x { get; set; }
//    ////        public int y { get; set; }
//    ////        public Vec2(int x, int y)
//    ////        {
//    ////            this.x = x;
//    ////            this.y = y;
//    ////        }
//    ////        public override string ToString()
//    ////        {
//    ////            return "(" + x + "," + y + ")";
//    ////        }
//    ////    }

//    ////    public static class SystemHandler
//    ////    {
//    ////        //public static ISystem[] Systems = new ISystem[] { 
//    ////        //new S_Position(),
//    ////        //new S_BasicPhys()
//    ////        //};
//    ////        public static S_Position positions = new S_Position();
//    ////        public static S_BasicPhys physics = new S_BasicPhys();
//    ////        public static void UpdateAll()
//    ////        {

//    ////            physics.UpdateAll();
//    ////            positions.UpdateAll();
//    ////            //for(int i = 0; i< Systems.Count() i++)
//    ////            //{
//    ////            //    Systems[i].UpdateAll();
//    ////            //}
//    ////        }

//    ////        static void ModifyInPlace<T>(this T a, T b) where T : IFormattable, IComparable, IConvertible
//    ////        {
//    ////            int foo;
//    ////            //a += b;
//    ////        }
//    ////    }

//    ////    //public struct BasicVel
//    ////    //{
//    ////    //    Vec2 position;
//    ////    //    Vec2 velocity;
//    ////    //}

//    ////    public class S_BasicPhys : System<Vec2>
//    ////    {
//    ////        void foo(ref Vec2 v2)
//    ////        {

//    ////        }
//    ////        protected override void Update(int id, Vec2 vel)
//    ////        {
//    ////            SystemHandler.positions[id].x += vel.x;
//    ////            SystemHandler.positions[id].y += vel.y;
//    ////            //SystemHandler.posSystem.Get(id).x += vel.x;
//    ////        }
//    ////    }
//    ////    public class S_Position : System<Vec2>
//    ////    {
//    ////        protected override void Update(int id, Vec2 component)
//    ////        {
//    ////            Console.WriteLine("Iteratin' -" + id + " " + component);
//    ////        }
//    ////    }

//    ////    public abstract class System<T> : ISystem where T : class
//    ////    {
//    ////        Dictionary<int, T> Components = new Dictionary<int, T>();

//    ////        public T this[int i]
//    ////        {
//    ////            get
//    ////            {
//    ////                return Components[i];
//    ////            }
//    ////            set
//    ////            {
//    ////                Components[i] = value;
//    ////            }
//    ////        }

//    ////        public T Get(int id)
//    ////        {
//    ////            return Components[id];
//    ////        }
//    ////        public void Set(int id, T comp)
//    ////        {
//    ////            Components[id] = comp;
//    ////        }
//    ////        public void Set(KeyValuePair<int, T> kvp)
//    ////        {
//    ////            Components[kvp.Key] = kvp.Value;
//    ////        }
//    ////        //public void Add (int i)
//    ////        //{
//    ////        //    Components.Add(i, new T());
//    ////        //}
//    ////        //public void Add(int i, T obj)
//    ////        //{
//    ////        //    Components.Add(i, obj);
//    ////        //}
//    ////        public void UpdateAll()
//    ////        {
//    ////            ArrayPool
//    ////            foreach (KeyValuePair<int, T> t in Components)
//    ////            {
//    ////                Update(t.Key, t.Value);
//    ////            }
//    ////        }
//    ////        protected abstract void Update(int id, T component);
//    ////    }

//    ////    public interface ISystem
//    ////    {
//    ////        void UpdateAll();
//    ////    }

//    ////}

//}

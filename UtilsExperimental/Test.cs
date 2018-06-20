//using System;
//using System.Collections.Concurrent;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Reactive;
//using System.Reactive.Linq;
//using System.Reactive.Subjects;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Threading;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Concurrent;

namespace foo
{
    //public class StructBorrow<T> : IDisposable where T:struct  
    //{
    //    public T  value;
    //    StructBorrow(ref T value)
    //    {
    //        this.value = value;
    //    }

    //    public void Dispose()
    //    {
    //        value = value;
    //    }
    //}

    struct PO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void react()
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("this"));
            //Observable.Create<int>((x) => { return Observer.; });
        }
    }

    class MyDataSource
    {

        private ObservableCollection<int> myData;
        public IObservable<int[]> DataChanged { get; private set; }
        public IObservable<ConsoleColor> SuccessColor { get; private set; }

        public MyDataSource()
        {

            DataChanged = Observable
                    .FromEvent<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                        handler => (sender, args) => handler(args),
                        handler => myData.CollectionChanged += handler,
                        handler => myData.CollectionChanged -= handler)
                    .Select(args => myData.ToArray());

            SuccessColor = DataChanged
                .Select(args => myData.Count > 0 && myData.Count < 10 ? ConsoleColor.Green : ConsoleColor.Red)
                .StartWith(ConsoleColor.Red);
        }
    }

    class Foo
    {
        void personalTest()
        {
            //BehaviorSubject<int> wowsers = new BehaviorSubject<int>(100);
            //wowsers.
        }
        //public IObservable<int> health = Observable.Create<int>(x => { }); 
        void practice()
        {
            PO yo = new PO() { };
            //int hp = new Subject<int>();
            int hp = 100;
            var rhp = Observable.Return(hp);
            //rhp.Subscribe(Observable.DistinctUntilChanged(rhp));
            rhp.Any((int x) => x == 0).Subscribe(null, () => Console.WriteLine("Dead!"));

            
        }

        private static IEnumerable<T> Unfold<T>(T seed, Func<T, T> accumulator)
        {
            var nextValue = seed;
            while (true)
            {
                yield return nextValue;
                nextValue = accumulator(nextValue);
            }
        }

        Foo() { 
            var hp = new Subject<int>();
            
            hp.OnNext(30);
            
            var values = new Subject<int>();
            var firstSubscription = values.Subscribe(value =>
            Console.WriteLine("1st subscription received {0}", value));
            var secondSubscription = values.Subscribe(value =>
            Console.WriteLine("2nd subscription received {0}", value));

            var thirsdSubscription = values.Subscribe(Console.WriteLine, () => { Console.WriteLine("YAY! Complete!"); });

            values.OnNext(0);
            values.OnNext(1);
            values.OnNext(2);
            values.OnNext(3);
            firstSubscription.Dispose();
            Console.WriteLine("Disposed of 1st subscription");
            values.OnNext(4);
            values.OnNext(5);
            values.OnCompleted();
            values.OnNext(6);

            // equivalent to subject above
            var singleval = Observable.Return("Single value then completed");
        }

        //Lazy evaluation. Preferred method for creation?
        private IObservable<string> NonBlockingLazy()
        {
            return Observable.Create<string>(
            (IObserver<string> observer) =>
            {
                observer.OnNext("a");
                observer.OnNext("b");
                observer.OnCompleted();
                
                Thread.Sleep(1000);
                return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed"));
    //or can return an Action like 
    //return () => Console.WriteLine("Observer has unsubscribed"); 
});

        }

        //Example code only
        public void NonBlocking_event_driven<T>(Action action, int interval = 1000  )
        {
            var ob = Observable.Create<string>(
            observer =>
            {
                var timer = new System.Timers.Timer();
                timer.Interval = interval;
                timer.Elapsed += (s, e) => observer.OnNext("tick");
                timer.Elapsed += OnTimerElapsed;
                timer.Elapsed += (s,e)=> action.Invoke();
                timer.Start();
                return timer;
            });
            var subscription = ob.Subscribe(Console.WriteLine);
            Console.ReadLine();
            subscription.Dispose();

            List<int> fools = new List<int>();
            var rfools = fools.ToObservable();
            
            //var range = Observable.
            //return ob;
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
        }

    }
}


//public static IObservable<T> Empty<T>()
//{
//    return Observable.Create<T>(o =>
//    {
//        o.OnCompleted();
//        return Disposable.Empty;
//    });
//}
//public static IObservable<T> Return<T>(T value)
//{
//    return Observable.Create<T>(o =>
//    {
//        o.OnNext(value);
//        o.OnCompleted();
//        return Disposable.Empty;
//    });
//}
//public static IObservable<T> Never<T>()
//{
//    return Observable.Create<T>(o =>
//    {
//        return Disposable.Empty;
//    });
//}
//public static IObservable<T> Throws<T>(Exception exception)
//{
//    return Observable.Create<T>(o =>
//    {
//        o.OnError(exception);
//        return Disposable.Empty;
//    });
//}













//namespace ObjectPoolExample
//{
//    struct foo
//    {
//        int bar;

//        //public void access( Func<int>())
//        //{

//        //}
//    }


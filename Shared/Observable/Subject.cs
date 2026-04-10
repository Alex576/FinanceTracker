using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Observable
{
    public class Subject<TData> : IObserver<TData>, IObservable<TData> where TData : class
    {
        private readonly HashSet<IObserver<TData>> _observers = new();
        private bool _isStopped;

        public void OnCompleted()
        {
            if (_isStopped)
                return;
            foreach (var observer in _observers.ToArray())
                observer.OnCompleted();
            _isStopped = true;
        }

        public void OnError(Exception error)
        {
            if (_isStopped)
                return;
            foreach (var observer in _observers.ToArray())
                observer.OnError(error);
            _isStopped = true;
        }

        public void OnNext(TData value)
        {
            if (_isStopped)
                return;
            foreach (var observer in _observers.ToArray())
                observer.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<TData> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber(() => _observers.Remove(observer));
        }

        public IObservable<TData> AsObservable() => this;
    }
}

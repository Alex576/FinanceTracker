using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Observable
{
    public static class SubjectExtensions
    {
        public static IObservable<TData> Filter<TData>(this IObservable<TData> source, Func<TData, bool> predicate)
        {
            return new AnonymousObservable<TData>(observer =>
            {
                return source.Subscribe(new AnonymousObserver<TData>(
                    value => { if (predicate(value)) observer.OnNext(value); },
                    error => observer.OnError(error),
                    () => observer.OnCompleted()
                ));
            });
        }
    }

    public class AnonymousObservable<T> : IObservable<T>
    {
        private readonly Func<IObserver<T>, IDisposable> _subscribe;
        public AnonymousObservable(Func<IObserver<T>, IDisposable> subscribe) => _subscribe = subscribe;
        public IDisposable Subscribe(IObserver<T> observer) => _subscribe(observer);
    }

    public class AnonymousObserver<T>(Action<T> onNext, Action<Exception>? onError = null, Action? onCompleted = null) : IObserver<T>
    {
        private readonly Action<T> _onNext = onNext;
        private readonly Action<Exception> _onError = onError ?? (e => { });
        private readonly Action _onCompleted = onCompleted ?? (() => { });

        public void OnNext(T value) => _onNext(value);
        public void OnError(Exception error) => _onError(error);
        public void OnCompleted() => _onCompleted();
    }
}

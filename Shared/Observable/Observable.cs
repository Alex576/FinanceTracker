using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Observable
{
    public class Observable<TData> : IObserver<TData> where TData : class
    {
        public Observable(Subject<TData> subject)
        {
            
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(TData value)
        {
            throw new NotImplementedException();
        }
    }
}

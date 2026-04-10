using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Observable
{
    public class Unsubscriber: IDisposable
    {
        private readonly Action _unsubscribe;
        public Unsubscriber(Action unsubscribe) => _unsubscribe = unsubscribe;
        public void Dispose() => _unsubscribe();
    }
}

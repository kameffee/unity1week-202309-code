using System;
using UniRx;

namespace Kameffee.Extensions
{
    public class Presenter : IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        public void AddDisposable(IDisposable item)
        {
            _disposable.Add(item);
        }

        void IDisposable.Dispose() => _disposable.Dispose();
    }
}
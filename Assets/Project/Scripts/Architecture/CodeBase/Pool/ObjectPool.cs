using System;
using System.Collections.Concurrent;

namespace Project.Scripts.Architecture.CodeBase.Pool {
  public class ObjectPool<T> where T : IPoolable {
    private readonly ConcurrentBag<T> _objects;
    private readonly Func<T> _objectGenerator;

    protected ObjectPool(Func<T> objectGenerator) {
      _objectGenerator = objectGenerator ?? throw new ArgumentNullException(nameof(objectGenerator));
      _objects = new ConcurrentBag<T>();
    }

    public T Get() {
      _objects.TryTake(out T item);
      item = item == null ? _objectGenerator() : item;
      item.Get();
      item.OnReturnEvent += Return;
      return item;
    }

    private void Return(object sender, EventArgs args) {
      var item = (T)sender;
      item.OnReturnEvent -= Return;
      _objects.Add(item);
    }
  }
}
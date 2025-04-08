using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Scripts.Architecture.CodeBase.Services.Factory {
  public class GameFactory : IGameFactory {
    private readonly DiContainer _container;
    private Dictionary<Type, FactoryPrefab> _prefabMap;

    public GameFactory(DiContainer container) {
      _container = container;
      Init();
    }

    public T Create<T>() where T : FactoryPrefab {
      var returnedObject = Object.Instantiate(_prefabMap[typeof(T)]) as T;
      _container.Inject(returnedObject);
      return returnedObject;
    }

    public void Init() {
      _prefabMap = Resources.LoadAll<FactoryPrefab>("Poolable").ToDictionary(prefab => prefab.GetType(), prefab => prefab);
    }

    public void Cleanup() { }

    public Task<T> CreateAsync<T>() where T : FactoryPrefab {
      if (!_prefabMap.TryGetValue(typeof(T), out FactoryPrefab prefab)) {
        Debug.LogError($"Prefab for type {typeof(T)} not found.");
        return Task.FromResult<T>(null);
      }

      return InstantiateAsync<T>(prefab.gameObject);
    }

    private Task<T> InstantiateAsync<T>(GameObject prefab) where T : FactoryPrefab {
      var tcs = new TaskCompletionSource<T>();

      AsyncInstantiateOperation<GameObject> asyncOp = Object.InstantiateAsync(prefab);

      asyncOp.completed += operation => {
                             GameObject[] results = asyncOp.Result;

                             if (results == null || results.Length == 0 || results[0] == null) {
                               tcs.SetException(new Exception("Instantiation failed: result is null or empty."));
                               return;
                             }

                             GameObject go = results[0];
                             var component = go.GetComponent<T>();
                             if (component == null) {
                               tcs.SetException(new Exception($"Component of type {typeof(T)} not found on instantiated prefab."));
                               return;
                             }

                             _container.Inject(component);
                             tcs.SetResult(component);
                           };

      return tcs.Task;
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
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
      _prefabMap = Resources.LoadAll<FactoryPrefab>("Poolable").ToDictionary(x => x.GetType(), x => x);
    }

    public void Cleanup() { }
  }
}
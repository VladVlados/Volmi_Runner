using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Architecture.CodeBase.ConstLogic;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.Services.Factory {
  public class TileViewFactory : ITileViewFactory {
    private readonly DiContainer _container;
    private Dictionary<TileViewType, TileView> _prefabMap;

    public TileViewFactory(DiContainer container) {
      _container = container;
      Init();
    }

    public T Create<T>(TileViewType type) where T : TileView {
      var returnedObject = Object.Instantiate(_prefabMap[type]) as T;
      _container.Inject(returnedObject);
      return returnedObject;
    }

    private void Init() {
      _prefabMap = Resources.LoadAll<TileView>(Constants.Paths.POOLABLE_PATH).ToDictionary(prefab => prefab.TileViewType, prefab => prefab);
    }
  }
}
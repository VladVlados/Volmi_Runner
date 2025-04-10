using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Architecture.CodeBase.ConstLogic;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.Services.Factory {
  public class ObstacleViewFactory : IObstacleViewFactory {
    private readonly DiContainer _container;
    private Dictionary<ObstacleName, ObstacleView> _namePrefabMap;
    private Dictionary<ObstacleType, ObstacleNames> _typePrefabMap;

    public ObstacleViewFactory(DiContainer container) {
      _container = container;
      Init();
    }

    public T Create<T>(ObstacleName name) where T : ObstacleView {
      var returnedObject = Object.Instantiate(_namePrefabMap[name]) as T;
      _container.Inject(returnedObject);
      return returnedObject;
    }

    public T Create<T>(ObstacleType type) where T : ObstacleView {
      List<ObstacleName> names = _typePrefabMap[type].Names;
      var returnedObject = Object.Instantiate(_namePrefabMap[names[Random.Range(0, names.Count)]]) as T;
      _container.Inject(returnedObject);
      return returnedObject;
    }

    private void Init() {
      _namePrefabMap = Resources.LoadAll<ObstacleView>(Constants.Paths.FACTORY_ITEMS).ToDictionary(prefab => prefab.ObstacleInfo.ObstacleName, prefab => prefab);
      _typePrefabMap = new Dictionary<ObstacleType, ObstacleNames>();
      
      foreach (KeyValuePair<ObstacleName, ObstacleView> view in _namePrefabMap) {
        ObstacleType type = view.Value.ObstacleInfo.Type;
        ObstacleName name = view.Value.ObstacleInfo.ObstacleName;
        if (_typePrefabMap.ContainsKey(type)) {
          _typePrefabMap[type].Names.Add(name);
        } else {
          _typePrefabMap.Add(type, new ObstacleNames {
            Names = new List<ObstacleName> {
              name
            }
          });
        }
      }
    }
  }

  public class ObstacleNames {
    public List<ObstacleName> Names;
  }
}
using System;
using System.Collections.Generic;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Level {
  public class TileView : FactoryPoolablePrefab {
    [SerializeField]
    private BoxCollider _tileCollider;
    [SerializeField]
    private TileSpawnPoints _spawnPoints;
    [SerializeField]
    private TileViewType _type;

    private ObstacleView _obstacleView;
    
    public void AddObstacle(ObstacleView obstacleView) {
      _obstacleView = obstacleView;
    }

    public void RemoveObstacle() {
      if (_obstacleView != null) {
        _obstacleView.Return();
        _obstacleView = null;
      }
    }

    public TileSpawnPoints TileSpawnPoints {
      get {
        return _spawnPoints;
      }
    }
    public BoxCollider Collider {
      get {
        return _tileCollider;
      }
    }

    public TileViewType TileViewType {
      get {
        return _type;
      }
    }
  }

  [Serializable]
  public class TileSpawnPoints {
    [SerializeField]
    private List<Transform> _spawnPoints;

    public Transform GetRandomPint() {
      return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }

    public List<Transform> SpawnPoints {
      get {
        return _spawnPoints;
      }
    }
  }

  public enum TileViewType {
    Road,
  }
}
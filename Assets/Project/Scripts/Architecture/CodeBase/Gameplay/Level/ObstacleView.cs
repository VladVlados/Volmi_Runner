using System;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Level {
  public class ObstacleView : FactoryPoolablePrefab , IInteractable {
    [SerializeField]
    private ObstacleInfo _info;
    [SerializeField]
    private MeshRenderer _mesh;
    [SerializeField]
    private bool _randomRotationAvailable;

    public void SetupRandomRotation() {
      if (!_randomRotationAvailable) {
        return;
      }

      float randomX = Random.Range(0f, 360f);

      Vector3 rotation = _mesh.gameObject.transform.eulerAngles;

      rotation.x = randomX;

      _mesh.gameObject.transform.eulerAngles = rotation;
    }

    public ObstacleInfo ObstacleInfo {
      get {
        return _info;
      }
    }

    public void OnInteractable() {
      Return();
    }
  }

  [Serializable]
  public class ObstacleInfo {
    [SerializeField]
    private ObstacleType _type;
    [SerializeField]
    private ObstacleName _name;

    public ObstacleType Type {
      get {
        return _type;
      }
    }

    public ObstacleName ObstacleName {
      get {
        return _name;
      }
    }
  }

  public enum ObstacleType {
    Reward = 0,
    Barrier = 1,
    Booster = 2
  }

  public enum ObstacleName {
    Apple = 0,
    Banana = 1,
    Cake = 2,
    Billboard = 3,
    PhoneBooth = 4,
    WheelieBin = 5
  }
}
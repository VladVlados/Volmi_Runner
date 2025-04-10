using System;
using System.Collections.Generic;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types {
  [CreateAssetMenu(fileName = "LevelConfigurations", menuName = "GlobalData/LevelConfigurations")]
  public class LevelConfigurations : ItemGlobalData {
    [SerializeField]
    private List<LevelConfiguration> _configurations;

    public ConfigurationInfo GetConfiguration(ConfigurationType type) {
      return _configurations.Find(levelConfig => levelConfig.ConfigurationType == type).ConfigurationInfo;
    }
  }

  [Serializable]
  public class LevelConfiguration {
    [SerializeField]
    private ConfigurationType _type;
    [SerializeField]
    private ConfigurationInfo _info;

    public ConfigurationType ConfigurationType {
      get {
        return _type;
      }
    }

    public ConfigurationInfo ConfigurationInfo {
      get {
        return _info;
      }
    }
  }

  [Serializable]
  public class ConfigurationInfo {
    [SerializeField]
    private int _tileCount;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _firstObstacleIndex;
    [SerializeField]
    private ObstacleName[] _targetRewards;

    public int TileCount {
      get {
        return _tileCount;
      }
    }
    public float Speed {
      get {
        return _speed;
      }
    }

    public int FirstObstacleIndex {
      get {
        return _firstObstacleIndex;

      }
    }

    public ObstacleName[] TargetRewards {
      get {
        return _targetRewards;
      }
    }
  }

  public enum ConfigurationType {
    Ez,
    Normal,
    Hard
  }
}
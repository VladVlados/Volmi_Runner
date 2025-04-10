using System;
using System.Collections.Generic;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types {
  [CreateAssetMenu(fileName = "RewardConfigurations", menuName = "GlobalData/RewardConfigurations")]
  public class RewardConfigurations : ItemGlobalData {
    [SerializeField]
    private List<RewardConfig> _rewardConfigs;

    public int GetPointsByReward(ObstacleName rewardName) {
      return _rewardConfigs.Find(rewardConfig => rewardConfig.ObstacleName == rewardName).Points;
    }
    public Sprite GetRewardSprite(ObstacleName rewardName) {
      return _rewardConfigs.Find(rewardConfig => rewardConfig.ObstacleName == rewardName).UISprite;
    }
  }

  [Serializable]
  public class RewardConfig {
    [SerializeField]
    private ObstacleName _name;
    [SerializeField]
    private int _points;
    [SerializeField]
    private Sprite _uiSprite;

    public ObstacleName ObstacleName {
      get {
        return _name;
      }
    }

    public int Points {
      get {
        return _points;
      }
    }

    public Sprite UISprite {
      get {
        return _uiSprite;
      }
    }
  }
}
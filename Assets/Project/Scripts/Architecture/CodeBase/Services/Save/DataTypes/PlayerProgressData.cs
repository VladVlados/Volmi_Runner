using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;

namespace Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes {
  [Serializable]
  public class PlayerProgressData : PlayerSavedData {
    private ConfigurationType _levelConfiguration;
    private List<MatchResult> _results = new();

    public void SetLevelConfiguration(ConfigurationType type) {
      _levelConfiguration = type;
      Save();
    }

    public void AddedNewResult(MatchResult result) {
      _results.Add(result);
      Save();
    }
    
    public ConfigurationType ConfigurationType {
      get {
        return _levelConfiguration;
      }
    }


    public List<MatchResult> Results {
      get {
        return _results;
      }
    }
  }

  [Serializable]
  public class MatchResult {
    public int TotalScore;
    public RewardsInfo Info = new();
  }

  [Serializable]
  public class RewardsInfo {
    public void AddReward(ObstacleName obstacleName) {
      var rewardInfo = Infos.FirstOrDefault(info => info.Name == obstacleName);
      if ( rewardInfo == null) {
        Infos.Add(new RewardInfo() {
          Name = obstacleName,
          Count = 1
        });
      } else {
        rewardInfo.Count++;
      }
    }
    public List<RewardInfo> Infos = new();
  }
  
  [Serializable]
  public class RewardInfo {
    public ObstacleName Name;
    public int Count;
  }
}
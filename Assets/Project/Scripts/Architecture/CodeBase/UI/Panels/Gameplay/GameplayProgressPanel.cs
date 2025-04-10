using System.Collections.Generic;
using DG.Tweening;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Pool;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay {
  public class GameplayProgressPanel : UIPanel {
    private readonly Dictionary<ObstacleName, RewardItemViewUI> _rewardItemView = new();

    [SerializeField]
    private RectTransform _rewardItemContainer;
    [SerializeField]
    private TMP_Text _totalScore;
    
    private RewardItemViewUIPool _rewardItemViewUIPool;

    public override void Init(DiContainer container) {
      base.Init(container);
      _rewardItemViewUIPool = new RewardItemViewUIPool(container.Resolve<IGameFactory>().Create<RewardItemViewUI>);
    }

    public void UpdateView(MatchResult result) {
      SetScore(result.TotalScore);

      foreach (RewardInfo rewardInfo in result.Info.Infos) {
        _rewardItemView[rewardInfo.Name].UpdateInfo(rewardInfo.Name, rewardInfo.Count);
      }
    }

    public void PrepareView(ObstacleName[] names) {
      SetScore(0);
      foreach (ObstacleName obstacleName in names) {
        if (_rewardItemView.TryGetValue(obstacleName, out RewardItemViewUI value)) {
          value.UpdateInfo(obstacleName, 0);
        } else {
          RewardItemViewUI rewardItemView = _rewardItemViewUIPool.Get();
          rewardItemView.transform.SetParent(_rewardItemContainer);
          rewardItemView.UpdateInfo(obstacleName, 0);
          _rewardItemView.Add(obstacleName, rewardItemView);
        }
      }
    }

    private void SetScore(int score) {
      _totalScore.text = $"Points : {score}";
    }
  }
}
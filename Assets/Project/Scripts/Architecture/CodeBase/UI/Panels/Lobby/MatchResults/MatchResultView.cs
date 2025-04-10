using System.Collections.Generic;
using Project.Scripts.Architecture.CodeBase.Pool;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby.MatchResults {
  public class MatchResultView : FactoryPoolablePrefab {
    [SerializeField]
    private RectTransform _rewardContainer;
    [SerializeField]
    private TMP_Text _scoreText;

    private List<RewardItemViewUI> _rewardItemView = new();

    private RewardItemViewUIPool _rewardItemViewUIPool;

    [Inject]
    private void Construct(IGameFactory factory) {
      _rewardItemViewUIPool = new RewardItemViewUIPool(factory.Create<RewardItemViewUI>);
    }

    public void Fill(MatchResult playerProgressResult) {
      _scoreText.text = $"Score : {playerProgressResult.TotalScore.ToString()}";

      PrepareItems(playerProgressResult.Info.Infos.Count);
      for (var index = 0; index < _rewardItemView.Count; index++) {
        RewardItemViewUI rewardItemView = _rewardItemView[index];
        RewardInfo rewardInfo = playerProgressResult.Info.Infos[index];
        rewardItemView.UpdateInfo(rewardInfo.Name, rewardInfo.Count);
      }
    }

    private void PrepareItems(int targetSlotCount) {
      if (targetSlotCount > _rewardItemView.Count) {
        int viewMissingCount = targetSlotCount - _rewardItemView.Count;
        SpawnView(viewMissingCount);
      }

      if (targetSlotCount < _rewardItemView.Count) {
        HideExtraViews(targetSlotCount);
      }
    }

    private void HideExtraViews(int targetSlotCount) {
      for (int i = _rewardItemView.Count - 1; i >= targetSlotCount; i--) {
        _rewardItemView[i].Return();
        _rewardItemView.RemoveAt(i);
      }
    }

    private void SpawnView(int count) {
      for (var i = 0; i < count; i++) {
        RewardItemViewUI view = _rewardItemViewUIPool.Get();
        view.transform.SetParent(_rewardContainer);
        _rewardItemView.Add(view);
      }
    }
  }
}
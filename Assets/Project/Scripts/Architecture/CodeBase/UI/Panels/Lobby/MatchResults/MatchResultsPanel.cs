using System;
using System.Collections.Generic;
using Project.Scripts.Architecture.CodeBase.Pool;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using Project.Scripts.Architecture.CodeBase.Services.Save;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby.MatchResults {
  public class MatchResultsPanel : UIPanel {
    [SerializeField]
    private ResultsListView _resultsListView;

    private PlayerProgressData _playerProgress;

    public override void Init(DiContainer container) {
      base.Init(container);
      
      var dataProvider = container.Resolve<IDataProvider>();
      _playerProgress = dataProvider.GetSaveData().GetData<PlayerProgressData>();
      
      var gameFactory = container.Resolve<IGameFactory>();
      var resultViewPool = new MatchResultViewPool(() => gameFactory.Create<MatchResultView>());
      _resultsListView.Setup(resultViewPool);
    }

    public override void Open() {
      base.Open();
      UpdateView();
    }

    private void UpdateView() {
      _resultsListView.Fill(_playerProgress.Results);
    }
  }

  [Serializable]
  public class ResultsListView {
    [SerializeField]
    private RectTransform _contentContainer;


    private readonly List<MatchResultView> _resultViews = new();
    
    private MatchResultViewPool _viewPool;

    public void Setup(MatchResultViewPool viewPool) {
      _viewPool = viewPool;
    }

    public void Fill(List<MatchResult> playerProgressResults) {
      PrepareSlots(playerProgressResults.Count);
    }

    private void PrepareSlots(int targetSlotCount) {
      if (targetSlotCount > _resultViews.Count) {
        var viewMissingCount = targetSlotCount - _resultViews.Count;
        SpawnView(viewMissingCount);
      }

      if (targetSlotCount < _resultViews.Count) {
        HideExtraViews(targetSlotCount);
      }
    }

    private void HideExtraViews(int targetSlotCount) {
      for (int i = _resultViews.Count - 1; i >= targetSlotCount; i--) {
        _resultViews[i].Return();
        _resultViews.RemoveAt(i);
      }
    }
    
    private void SpawnView(int count) {
      for (int i = 0; i < count; i++) {
        var view = _viewPool.Get();
        view.transform.SetParent(_contentContainer);
        _resultViews.Add(view);
      }
    }
  }
}
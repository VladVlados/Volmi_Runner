using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Architecture.CodeBase.Pool;
using Project.Scripts.Architecture.CodeBase.Services.Factory;
using Project.Scripts.Architecture.CodeBase.Services.Save;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby.MatchResults {
  public class MatchResultsPanel : UIPanel {
    [SerializeField]
    private ResultsListView _resultsListView;
    [SerializeField]
    private Button _closeButton;

    private PlayerProgressData _playerProgress;

    private void Awake() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

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

    private void AddListeners() {
      _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void RemoveListeners() {
      _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick() {
      UiManager.Hide(this);
    }

    private void UpdateView() {
      List<MatchResult> sortedResults = _playerProgress.Results.OrderByDescending(result => result.TotalScore).ToList();
      _resultsListView.Fill(sortedResults);
    }
  }

  [Serializable]
  public class ResultsListView {
    private readonly List<MatchResultView> _resultViews = new();
    [SerializeField]
    private RectTransform _contentContainer;

    private MatchResultViewPool _viewPool;

    public void Setup(MatchResultViewPool viewPool) {
      _viewPool = viewPool;
    }

    public void Fill(List<MatchResult> playerProgressResults) {
      PrepareSlots(playerProgressResults.Count);

      for (var index = 0; index < _resultViews.Count; index++) {
        MatchResultView matchResultView = _resultViews[index];
        matchResultView.Fill(playerProgressResults[index]);
      }
    }

    private void PrepareSlots(int targetSlotCount) {
      if (targetSlotCount > _resultViews.Count) {
        int viewMissingCount = targetSlotCount - _resultViews.Count;
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
      for (var i = 0; i < count; i++) {
        MatchResultView view = _viewPool.Get();
        view.transform.SetParent(_contentContainer);
        _resultViews.Add(view);
      }
    }
  }
}
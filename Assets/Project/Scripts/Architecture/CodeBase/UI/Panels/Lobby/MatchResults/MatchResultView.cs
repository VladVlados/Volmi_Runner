using Project.Scripts.Architecture.CodeBase.Services.Factory;
using TMPro;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.UI.Panels.Lobby.MatchResults {
  public class MatchResultView : FactoryPoolablePrefab {
    [SerializeField]
    private RectTransform _rewardContainer;
    [SerializeField]
    private TMP_Text _scoreText;
  }
}
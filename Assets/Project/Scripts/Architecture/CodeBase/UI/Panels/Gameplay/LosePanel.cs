using System;
using Project.Scripts.Architecture.CodeBase.Services.Save.DataTypes;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay {
  public class LosePanel : UIPanel {
    public event Action OnLobbyButtonClick;
    
    [SerializeField]
    private TMP_Text _finalScore;
    [SerializeField]
    private Button _lobbyButton;

    private MatchResult _result;

    private void Awake() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    public void SetupResult(MatchResult result) {
      _result = result;
      UpdateView();
    }

    private void RemoveListeners() {
      _lobbyButton.onClick.RemoveListener(LobbyButtonClick);
    }

    private void AddListeners() {
      _lobbyButton.onClick.AddListener(LobbyButtonClick);
    }

    private void LobbyButtonClick() {
      OnLobbyButtonClick?.Invoke();
    }

    private void UpdateView() {
      _finalScore.text = $"Your score : {_result.TotalScore.ToString()}";
    }
  }
}
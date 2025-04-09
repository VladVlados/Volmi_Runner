using Project.Scripts.Architecture.CodeBase.Services.Factory;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.Lobby {
  public interface ILobbyEnvironmentHandler {
    void ShowLobbyEnvironment(EnvironmentName targetEnvironment);
  }

  public class LobbyEnvironmentHandler : IService, ILobbyEnvironmentHandler {
    private readonly IGlobalDataService _globalDataService;
    private readonly IGameFactory _gameFactory;

    private LobbyEnvironmentsVariable _lobbyEnvironments;
    private GameObject _currentEnvironment;

    public LobbyEnvironmentHandler(IGlobalDataService globalDataService) {
      _globalDataService = globalDataService;
      ShowLobbyEnvironment(EnvironmentName.Default);
    }

    public void ShowLobbyEnvironment(EnvironmentName targetEnvironment) {
      LobbyEnvironment lobbyEnvironment = LobbyEnvironments.FindEnvironmentByName(targetEnvironment);
      Object.Destroy(_currentEnvironment);
      CreateEnvironment(lobbyEnvironment);
    }

    private void CreateEnvironment(LobbyEnvironment lobbyEnvironment) {
      _currentEnvironment = Object.Instantiate(lobbyEnvironment.Prefab);
    }

    private LobbyEnvironmentsVariable LobbyEnvironments {
      get {
        return _lobbyEnvironments ??= _globalDataService.GetGlobalData<LobbyEnvironmentsVariable>();
      }
    }
  }
}
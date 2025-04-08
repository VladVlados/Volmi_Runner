using Project.Scripts.Architecture.CodeBase.ConstLogic;
using Project.Scripts.Architecture.CodeBase.Services.SceneLoader;
using Project.Scripts.Architecture.CodeBase.UI.LoadScreen;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States.GameSetupStates {
  public class LobbyLoadingState : State {
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoaderScreenService _loaderScreenService;

    public LobbyLoadingState(ISceneLoader sceneLoader, ILoaderScreenService loaderScreenService)  {
      _sceneLoader = sceneLoader;
      _loaderScreenService = loaderScreenService;
    }

    public override void Enter() {
      _sceneLoader.Load(Constants.SceneNames.LOBBY, OnSceneLoaded);
      _loaderScreenService.StartIntro();
      Debug.Log($"Scene {Constants.SceneNames.LOBBY} Loading ---STARTED---");
    }

    private void OnSceneLoaded() {
      Debug.Log("Scene Loading ---FINISHED---");
      _loaderScreenService.HideIntro();
      _gameStateMachine.Enter<LobbySceneState>();
      IsActive = false;
    }
  }
}
using Project.Scripts.Architecture.CodeBase.ConstLogic;
using Project.Scripts.Architecture.CodeBase.Services.SceneLoader;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using Project.Scripts.Architecture.CodeBase.UI.LoadScreen;
using Project.Scripts.Architecture.CodeBase.UI.Panels.Gameplay;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States.Gameplay {
  public class GameplayLoadingState: State {
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoaderScreenService _loaderScreenService;
    private readonly IUIManager _uiManager;

    public GameplayLoadingState(ISceneLoader sceneLoader, ILoaderScreenService loaderScreenService, IUIManager uiManager){
      _sceneLoader = sceneLoader;
      _loaderScreenService = loaderScreenService;
      _uiManager = uiManager;
    }

    public override void Enter() {
      _sceneLoader.Load(Constants.SceneNames.GAMEPLAY, OnSceneLoaded);
      _uiManager.HideAll();
      _uiManager.Show<GameplayNavigationPanel>();
      _loaderScreenService.StartIntro();
      Debug.Log($"Scene {Constants.SceneNames.GAMEPLAY} Loading ---STARTED---");
    }

    private void OnSceneLoaded() {
      Debug.Log("Scene Loading ---FINISHED---");
      _gameStateMachine.Enter<GameplaySceneSetupState>();
      IsActive = false;
    }
  }
}
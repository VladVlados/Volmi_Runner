using Project.Scripts.Architecture.CodeBase.ConstLogic;
using Project.Scripts.Architecture.CodeBase.Services.SceneLoader;
using Project.Scripts.Architecture.CodeBase.UI.LoadScreen;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States {
  public class LoadSceneState : State {
    private readonly ISceneLoader _sceneLoader;
    private readonly ILoaderScreenService _loaderScreenService;

    public LoadSceneState(ISceneLoader sceneLoader, ILoaderScreenService loaderScreenService) {
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
      IsActive = false;
    }
  }
}
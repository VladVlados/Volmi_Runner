using System.Threading.Tasks;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using Project.Scripts.Architecture.CodeBase.UI.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.GameStates.States.GameSetupStates {
  public class InitUIManagerState : State {
    private readonly DiContainer _container;
    private readonly IGlobalDataService _globalDataService;
    private readonly IUIManager _uiManager;

    public InitUIManagerState(DiContainer container, IGlobalDataService globalDataService, IUIManager uiManager) {
      _globalDataService = globalDataService;
      _container = container;
      _uiManager = uiManager;
    }

    public override void Enter() {
      base.Enter();

      LoadSceneComponents();
    }

    private async void LoadSceneComponents() {
      (IUIManager uiManager, UIPanels panels) = await CreateComponents();
      PrepareUIManager(uiManager as UIManager, panels);

      Debug.Log("UI Manager loaded");
      IsActive = false;
    }

    private Task<(IUIManager, UIPanels)> CreateComponents() {
      var panels = _globalDataService.GetGlobalData<UIPanels>();
      return Task.FromResult((_uiManager, panels));
    }

    private void PrepareUIManager(UIManager uiManager, UIPanels panels) {
      uiManager.Init(panels.WarmUp(_container, uiManager.ContainerHided.transform));
    }
  }
}
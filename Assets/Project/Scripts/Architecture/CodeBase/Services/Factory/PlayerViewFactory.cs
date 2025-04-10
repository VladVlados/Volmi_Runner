using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Architecture.CodeBase.ConstLogic;
using Project.Scripts.Architecture.CodeBase.Gameplay.Player;
using Project.Scripts.Architecture.CodeBase.Signal;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.Services.Factory {
  public class PlayerViewFactory : IPlayerViewFactory {
    private readonly SignalBus _signalBus;
    private DiContainer _container;
    private Dictionary<PlayerViewType, PlayerView> _prefabMap;

    public PlayerViewFactory(DiContainer container, SignalBus signalBus) {
      _container = container;
      _signalBus = signalBus;
      _signalBus.Subscribe<SceneReadySignal>(OnSceneReady);
      Init();
    }

    ~PlayerViewFactory() {
      _signalBus.Unsubscribe<SceneReadySignal>(OnSceneReady);
    }

    public T Create<T>(PlayerViewType type) where T : PlayerView {
      PlayerView prefab = _prefabMap[type];
      GameObject instance = _container.InstantiatePrefab(prefab);
      return instance.GetComponent<T>();
    }

    private void OnSceneReady(SceneReadySignal signal) {
      _container = signal.SceneContainer;
    }

    private void Init() {
      _prefabMap = Resources.LoadAll<PlayerView>(Constants.Paths.FACTORY_ITEMS).ToDictionary(prefab => prefab.PlayerViewType, prefab => prefab);
    }
  }
}
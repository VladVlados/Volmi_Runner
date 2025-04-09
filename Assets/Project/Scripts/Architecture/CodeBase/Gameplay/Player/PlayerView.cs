using Project.Scripts.Architecture.CodeBase.Services.Factory;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Player {
  [RequireComponent(typeof(PlayerCollision))]
  public class PlayerView : FactoryPoolablePrefab {
    [SerializeField]
    private PlayerViewType _playerViewType;

    public void SelectAnimation() { }

    public PlayerViewType PlayerViewType {
      get {
        return _playerViewType;
      }
    }
  }
  
  public enum PlayerViewType {
    Biker,
    Clown
  }
}
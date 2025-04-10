using Project.Scripts.Architecture.CodeBase.Services.Factory;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Player {
  [RequireComponent(typeof(PlayerCollision))]
  public class PlayerView : FactoryPoolablePrefab {
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    
    [SerializeField]
    private PlayerViewType _playerViewType;
    [SerializeField]
    private Animator _animator;

    public void SelectAnimation(PlayerAnimationType type) {
      _animator.SetBool(IsRunning, type == PlayerAnimationType.Rum);
    }

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

  public enum PlayerAnimationType {
    Idle,
    Rum
  }
}
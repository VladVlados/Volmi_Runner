using System;
using Project.Scripts.Architecture.CodeBase.Gameplay.Level;
using Project.Scripts.Architecture.CodeBase.Gameplay.Meta;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Player {
  public class PlayerCollision : MonoBehaviour {
    private IPlayerCollisionHandler _playerCollisionHandler;
    
    [Inject]
    public void Construct(IPlayerCollisionHandler playerCollisionHandler) {
      _playerCollisionHandler = playerCollisionHandler;
    }

    private void OnTriggerEnter(Collider other) {
      if (other.TryGetComponent(out IInteractable interactable)) {
        _playerCollisionHandler.ProcessCollision(interactable);
      }
    }
  }
}
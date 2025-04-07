using UnityEngine;

namespace Architecture.CodeBase.Services.CoroutineHandler {
  public class CoroutineHandler : MonoBehaviour, ICoroutineHandler {
    private void OnEnable() {
      IsActive = true;
    }

    private void OnDisable() {
      IsActive = false;
    }

    public bool IsActive { get; private set; }
  }
}
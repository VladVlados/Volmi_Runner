using System;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.Events {
  public class MonoEventsProvider : MonoBehaviour, IMonoEventsService {
    
    public event EventHandler OnUpdate;
    public event EventHandler OnFixedUpdate;
    public event EventHandler OnLateUpdate;
    public event Action<bool> OnAppFocus;
    public event Action<bool> OnAppPause;
    public event Action OnAppQuit;

    private void Awake() {
      DontDestroyOnLoad(this);
    }

    private void Update() {
      OnUpdate?.Invoke(this, EventArgs.Empty);
    }

    private void FixedUpdate() {
      OnFixedUpdate?.Invoke(this, EventArgs.Empty);
    }

    private void LateUpdate() {
      OnLateUpdate?.Invoke(this, EventArgs.Empty);
    }

    private void OnApplicationFocus(bool hasFocus) {
      OnAppFocus?.Invoke(hasFocus);
    }

    private void OnApplicationPause(bool pauseStatus) {
      OnAppPause?.Invoke(pauseStatus);
    }

    private void OnApplicationQuit() {
      OnAppQuit?.Invoke();
    }
  }
}
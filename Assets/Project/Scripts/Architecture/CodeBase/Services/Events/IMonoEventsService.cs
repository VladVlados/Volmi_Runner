using System;

namespace Project.Scripts.Architecture.CodeBase.Services.Events {
  public interface IMonoEventsService : IService {
    event EventHandler OnUpdate;
    event EventHandler OnFixedUpdate;
    event EventHandler OnLateUpdate;
    event Action<bool> OnAppFocus;
    event Action<bool> OnAppPause;
    event Action OnAppQuit;
  }
}
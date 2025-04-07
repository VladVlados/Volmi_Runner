using System;

namespace Architecture.CodeBase.Services {
  public interface IService { }
  public interface IInitializedService : IService, IDisposable {
    void Initialize();
    bool IsInitialized { get; }
  }
}
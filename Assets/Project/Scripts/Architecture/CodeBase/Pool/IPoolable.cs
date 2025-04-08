using System;

namespace Project.Scripts.Architecture.CodeBase.Pool {
  public interface IPoolable {
    event EventHandler OnReturnEvent;

    void Get();
  }
}
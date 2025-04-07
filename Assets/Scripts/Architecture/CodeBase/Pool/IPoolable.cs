using System;

namespace Architecture.CodeBase.Pool {
  public interface IPoolable {
    event EventHandler OnReturnEvent;

    void Get();
  }
}
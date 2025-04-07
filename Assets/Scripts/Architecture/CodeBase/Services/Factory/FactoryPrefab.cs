using System;
using Architecture.CodeBase.Pool;
using UnityEngine;

namespace Architecture.CodeBase.Services.Factory {
  public class FactoryPrefab : MonoBehaviour { }

  public abstract class FactoryPoolablePrefab : FactoryPrefab, IPoolable {
    public event EventHandler OnReturnEvent;

    public virtual void Get() {
      gameObject.SetActive(true);
    }

    public virtual void Return() {
      gameObject.SetActive(false);
      OnReturnEvent?.Invoke(this, EventArgs.Empty);
    }
  }
}
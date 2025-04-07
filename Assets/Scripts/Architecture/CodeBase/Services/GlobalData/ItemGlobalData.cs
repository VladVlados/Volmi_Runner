using UnityEngine;

namespace Architecture.CodeBase.Services.GlobalData {
  public abstract class ItemGlobalData : ScriptableObject {
    public void LoadEssential() {
      LoadData();
    }

    protected virtual void LoadData() { }
  }
}
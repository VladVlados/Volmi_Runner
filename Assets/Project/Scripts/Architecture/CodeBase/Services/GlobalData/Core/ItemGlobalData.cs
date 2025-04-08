using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core {
  public abstract class ItemGlobalData : ScriptableObject {
    public void LoadEssential() {
      LoadData();
    }

    protected virtual void LoadData() { }
  }
}
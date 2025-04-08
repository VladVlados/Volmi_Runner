using System;

namespace Project.Scripts.Architecture.CodeBase.Services.Save {
  [Serializable]
  public class PlayerSavedData {
    [NonSerialized]
    private IDataSaver _saver;

    public virtual void Setup(IDataSaver saver) {
      _saver = saver;
    }

    protected void Save() {
      _saver.SaveGame();
    }
  }
}
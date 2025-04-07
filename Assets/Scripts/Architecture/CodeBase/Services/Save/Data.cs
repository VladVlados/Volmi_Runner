using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.CodeBase.Services.Save.DataTypes;

namespace Architecture.CodeBase.Services.Save {
  [Serializable]
  public class Data {
    private List<PlayerSavedData> _playerSavedData;

    public Data() {
      InitSavedDataList();
    }

    public T GetData<T>() where T : PlayerSavedData {
      return _playerSavedData.First(data => data.GetType() == typeof(T)) as T;
    }

    public void Setup(IDataSaver saver) {
      foreach (PlayerSavedData savedData in _playerSavedData) {
        savedData.Setup(saver);
      }
    }

    private void InitSavedDataList() {
      _playerSavedData = new List<PlayerSavedData> {
        new PlayerProgressData(),
      };
    }
  }
}
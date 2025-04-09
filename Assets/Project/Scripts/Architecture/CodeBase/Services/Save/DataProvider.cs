using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Project.Scripts.Architecture.CodeBase.ConstLogic;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.Save {
  public class DataProvider : IDataProvider {
    private Data _data;

    public DataProvider() {
      Initialize();
    }

    public Data GetSaveData() {
      return _data;
    }

    public void SaveGame() {
#if !UNITY_EDITOR
      string filePath = Application.persistentDataPath + Constants.Constants.Paths.DATA_PATH;
#else
      string filePath = Application.dataPath + Constants.Paths.DATA_PATH;
#endif

      using (FileStream file = File.Create(filePath)) {
        new BinaryFormatter().Serialize(file, _data);
      }
    }

    private void Initialize() {
#if !UNITY_EDITOR
      string filePath = Application.persistentDataPath + Constants.Paths.DATA_PATH;
#else
      string filePath = Application.dataPath + Constants.Paths.DATA_PATH;
#endif
      if (File.Exists(filePath)) {
        using (FileStream file = File.Open(filePath, FileMode.Open)) {
          object loadedData = new BinaryFormatter().Deserialize(file);
          _data = (Data)loadedData;
          _data.Setup(this);
        }

        return;
      }

      _data = new Data();
      using (FileStream file = File.Create(filePath)) {
        new BinaryFormatter().Serialize(file, _data);
      }
    }
  }
}
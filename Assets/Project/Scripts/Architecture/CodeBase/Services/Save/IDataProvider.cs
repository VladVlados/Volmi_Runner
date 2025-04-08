namespace Project.Scripts.Architecture.CodeBase.Services.Save {
  public interface IDataProvider : IDataLoader, IDataSaver { }

  public interface IDataLoader : IService {
    Data GetSaveData();
  }

  public interface IDataSaver : IService {
    void SaveGame();
  }
}
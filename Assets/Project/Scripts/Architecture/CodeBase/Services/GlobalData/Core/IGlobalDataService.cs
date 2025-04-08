namespace Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core {
  public interface IGlobalDataService : IInitializedService {
    T GetGlobalData<T>() where T : ItemGlobalData;
  }
}
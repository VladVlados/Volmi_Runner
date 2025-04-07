namespace Architecture.CodeBase.Services.GlobalData {
  public interface IGlobalDataService : IInitializedService {
    T GetGlobalData<T>() where T : ItemGlobalData;
  }
}
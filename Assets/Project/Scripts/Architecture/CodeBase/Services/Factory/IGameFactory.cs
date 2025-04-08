using System.Threading.Tasks;

namespace Project.Scripts.Architecture.CodeBase.Services.Factory {
  public interface IGameFactory : IService {
    T Create<T>() where T : FactoryPrefab;
    void Init();
    void Cleanup();
    Task<T> CreateAsync<T>() where T : FactoryPrefab;
  }
}
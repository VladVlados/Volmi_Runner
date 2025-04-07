using System.Threading.Tasks;
using UnityEngine;

namespace Architecture.CodeBase.Services.Factory {
  public interface IGameFactory : IService{
    T Create<T>() where T : FactoryPrefab;
    void Init();

    void Cleanup();
  }
}
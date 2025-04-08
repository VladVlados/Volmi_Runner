using System.Collections;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.CoroutineHandler {
  public interface ICoroutineHandler: IService{
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopCoroutine(Coroutine routine);
    bool IsActive { get; }
  }
}
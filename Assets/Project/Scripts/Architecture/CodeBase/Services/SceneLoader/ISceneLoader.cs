using System;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.SceneLoader {
  public interface ISceneLoader: IService {
    public void Load(string sceneName, Action onSceneLoaded = null);
  }
}
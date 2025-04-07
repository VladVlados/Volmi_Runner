using System;
using UnityEngine;

namespace Architecture.CodeBase.Services.SceneLoader {
  public interface ISceneLoader: IService {
    public Action OnStartLoad { get; set; }
    public string PreviousSceneName { get; set; }
    public void Load(string sceneName, Action onSceneLoaded = null, Action<AsyncOperation> onSceneProgressLoad = null , float minLoadingTime = 0f , bool reload = false);
  }
}
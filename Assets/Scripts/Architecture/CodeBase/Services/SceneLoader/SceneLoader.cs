using System;
using System.Collections;
using Architecture.CodeBase.Services.CoroutineHandler;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture.CodeBase.Services.SceneLoader {
  public class SceneLoader : ISceneLoader {
    private const float MAX_PRELOAD_SCENE_PROGRESS = 0.9f;

    private readonly ICoroutineHandler _coroutineHandler;

    private DateTime _startLoadingTime;

    public SceneLoader(ICoroutineHandler coroutineHandler) {
      _coroutineHandler = coroutineHandler;
    }

    public void Load(string sceneName, Action onSceneLoaded = null, Action<AsyncOperation> onSceneProgressLoad = null, float minLoadingTime = 0, bool reload = false) {
      _startLoadingTime = DateTime.Now;
      PreviousSceneName = SceneManager.GetActiveScene().name;
      OnStartLoad?.Invoke();
      _coroutineHandler.StartCoroutine(LoadScene(sceneName, onSceneLoaded, onSceneProgressLoad, minLoadingTime, reload));
    }

    public Action OnStartLoad { get; set; }

    public string PreviousSceneName { get; set; }

    private IEnumerator LoadScene(string nextScene, Action onSceneLoaded, Action<AsyncOperation> onSceneProgressLoad, float minLoadingTime, bool reload) {
      if (SceneManager.GetActiveScene().name.Equals(nextScene) && !reload) {
        onSceneLoaded?.Invoke();
        yield break;
      }

      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
      waitNextScene.allowSceneActivation = false;

      while (waitNextScene.progress < MAX_PRELOAD_SCENE_PROGRESS || (DateTime.Now - _startLoadingTime).TotalSeconds < minLoadingTime) {
        onSceneProgressLoad?.Invoke(waitNextScene);
        yield return null;
      }

      yield return new WaitForEndOfFrame();
      //BuildUI;
      waitNextScene.allowSceneActivation = true;

      onSceneLoaded?.Invoke();
    }
  }
}
using System;
using System.Collections;
using Project.Scripts.Architecture.CodeBase.Services.CoroutineHandler;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Architecture.CodeBase.Services.SceneLoader {
  public class SceneLoader : ISceneLoader {
    private readonly ICoroutineHandler _coroutineHandler;

    public SceneLoader(ICoroutineHandler coroutineHandler) {
      _coroutineHandler = coroutineHandler;
    }

    public void Load(string sceneName, Action onSceneLoaded = null) {
      _coroutineHandler.StartCoroutine(LoadScene(sceneName, onSceneLoaded));
    }

    private IEnumerator LoadScene(string nextScene, Action onSceneLoaded) {
      if (SceneManager.GetActiveScene().name.Equals(nextScene)) {
        onSceneLoaded?.Invoke();
        yield break;
      }

      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (waitNextScene.isDone == false) {
        yield return null;
      }

      onSceneLoaded?.Invoke();
    }
  }
}
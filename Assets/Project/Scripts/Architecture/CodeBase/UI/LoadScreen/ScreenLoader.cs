using System;
using System.Collections;
using Project.Scripts.Architecture.CodeBase.Services.CoroutineHandler;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.UI.LoadScreen {
  public class ScreenLoader : MonoBehaviour, ILoaderScreenService {
    private const float INTRO_TIME = 1.0f;

    [SerializeField]
    private ScreenLoaderAnimation _loaderAnimation;
    [SerializeField]
    private CanvasGroup _interactableGroup;

    private ICoroutineHandler _coroutineHandler;
    private Coroutine _introRoutine;

    [Inject]
    public void Construct(ICoroutineHandler coroutineHandler) {
      _coroutineHandler = coroutineHandler;
    }

    public void StartIntro() {
      _interactableGroup.interactable = true;
      _interactableGroup.blocksRaycasts = true;
      _interactableGroup.alpha = 1f;
      _introRoutine = _coroutineHandler.StartCoroutine(ShowIntroRoutine());
    }

    public void HideIntro() {
      _interactableGroup.interactable = false;
      _interactableGroup.blocksRaycasts = false;
      _interactableGroup.alpha = 0f;
      _coroutineHandler.StopCoroutine(_introRoutine);
      _loaderAnimation.Stop();
    }

    public bool IsIntroPlaying {
      get {
        return _loaderAnimation.IsPlaying;
      }
    }

    private IEnumerator ShowIntroRoutine() {
      _loaderAnimation.Play();
      yield return new WaitForSeconds(INTRO_TIME);
      _loaderAnimation.Stop();
    }
  }

  [Serializable]
  public class ScreenLoaderAnimation {
    [SerializeField]
    private Image _circleAnimation;

    public void Play() {
      _circleAnimation.gameObject.SetActive(true);
    }

    public void Stop() {
      _circleAnimation.gameObject.SetActive(false);
    }

    public bool IsPlaying {
      get {
        return _circleAnimation.enabled;
      }
    }
  }
}
using System;
using System.Collections;
using Project.Scripts.Architecture.CodeBase.Services.CoroutineHandler;
using Project.Scripts.Architecture.CodeBase.Services.Events;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Services.Lobby {
  public interface ISwipeDetector : IService {
    event Action SwipeUp;
    event Action SwipeDown;
    event Action SwipeLeft;
    event Action SwipeRight;
  }

  public class SwipeDetector : ISwipeDetector {
    private const float MIN_SWAP_DISTANCE = 0.2f;
    private const float DIRECTION_THRESHOLD = 0.9f;
    private const int AVAILABLE_TOUCH_COUNT = 1;

    private readonly ICoroutineHandler _coroutineHandler;
    private readonly IMonoEventsService _monoEventsService;

    public event Action SwipeUp;
    public event Action SwipeDown;
    public event Action SwipeLeft;
    public event Action SwipeRight;

    private Vector2 _startPosition;
    private Vector2 _lastPosition;
    private float _startTime;
    private float _endTime;

    public SwipeDetector(ICoroutineHandler coroutineHandler, IMonoEventsService monoEventsService) {
      _coroutineHandler = coroutineHandler;
      _monoEventsService = monoEventsService;
      AddListeners();
    }

    ~SwipeDetector() {
      RemoveListeners();
    }

    private void AddListeners() {
      _monoEventsService.OnUpdate += OnUpdate;
    }

    private void RemoveListeners() {
      _monoEventsService.OnUpdate -= OnUpdate;
    }

    private void OnUpdate(object sender, EventArgs e) {
#if UNITY_EDITOR
      if (Input.GetMouseButtonDown(0)) {
        _startPosition = Input.mousePosition;
        OnSwipe();
      }
#else
      if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
        _startPosition = Input.GetTouch(0).position;
        OnSwipe();
      }
#endif
    }

    private void OnSwipe() {
      _coroutineHandler.StartCoroutine(SwipeTracker());
    }

    private IEnumerator SwipeTracker() {
      while (ShouldTrack()) {
        yield return new WaitForEndOfFrame();
      }

      OnEndSwipe();
    }

    private bool ShouldTrack() {
#if UNITY_EDITOR
      if (Input.GetMouseButton(0)) {
        _lastPosition = Input.mousePosition;
        return true;
      }
#else
      if (TouchAvailable) {
        _lastPosition = Input.GetTouch(0).position;
        return true;
      }
#endif
      return false;
    }

    private void OnEndSwipe() {
      bool enoughDistance = Vector3.Distance(_startPosition, _lastPosition) > MIN_SWAP_DISTANCE;
      if (!enoughDistance) {
        return;
      }

      Vector3 direction = _lastPosition - _startPosition;
      Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
      SwipeDirection(direction2D);
    }

    private void SwipeDirection(Vector2 direction) {
      if (Vector2.Dot(Vector2.up, direction) > DIRECTION_THRESHOLD) {
        SwipeUp?.Invoke();
        return;
      }

      if (Vector2.Dot(Vector2.down, direction) > DIRECTION_THRESHOLD) {
        SwipeDown?.Invoke();
        return;
      }

      if (Vector2.Dot(Vector2.right, direction) > DIRECTION_THRESHOLD) {
        SwipeLeft?.Invoke();
        return;
      }

      if (Vector2.Dot(Vector2.left, direction) > DIRECTION_THRESHOLD) {
        SwipeRight?.Invoke();
      }
    }

    private bool TouchAvailable {
      get {
        return Input.touchCount == AVAILABLE_TOUCH_COUNT;
      }
    }
  }
}
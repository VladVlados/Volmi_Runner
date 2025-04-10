using System.Collections.Generic;
using DG.Tweening;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Core;
using Project.Scripts.Architecture.CodeBase.Services.GlobalData.Types;
using Project.Scripts.Architecture.CodeBase.Services.Lobby;
using UnityEngine;

namespace Project.Scripts.Architecture.CodeBase.Gameplay.Player {
  public interface IPlayerMoveController {
    void StartMoveInput();

    void StopMoveInput();

    void SetMoveWay(List<Vector3> availableMovePositions, int startIndex);
  }

  public class PlayerMoveController : IPlayerMoveController {
    private readonly ISwipeDetector _swipeDetector;
    private readonly IPlayerViewProvider _playerViewProvider;
    private readonly PlayerConfigurations _playerConfigurations;

    private List<Vector3> _availableMovePositions;
    private Tween _moveTween;
    private int _currentPositionIndex;

    public PlayerMoveController(ISwipeDetector swipeDetector, IPlayerViewProvider playerViewProvider , IGlobalDataService globalDataService) {
      _swipeDetector = swipeDetector;
      _playerViewProvider = playerViewProvider;
      _playerConfigurations = globalDataService.GetGlobalData<PlayerConfigurations>();
    }

    public void StartMoveInput() {
      _swipeDetector.SwipeLeft += OnSwipeLeft;
      _swipeDetector.SwipeRight += OnSwipeRight;
    }

    public void StopMoveInput() {
      _swipeDetector.SwipeLeft -= OnSwipeLeft;
      _swipeDetector.SwipeRight -= OnSwipeRight;
    }

    public void SetMoveWay(List<Vector3> availableMovePositions, int startIndex) {
      _availableMovePositions = availableMovePositions;
      _currentPositionIndex = startIndex;
      MoveToPoint(_currentPositionIndex);
    }

    private void OnSwipeRight() {
      if (IsMoving) {
        return;
      }

      _currentPositionIndex++;
      _currentPositionIndex = Mathf.Clamp(_currentPositionIndex, 0, _availableMovePositions.Count - 1);
      MoveToPoint(_currentPositionIndex);
    }

    private void OnSwipeLeft() {
      if (IsMoving) {
        return;
      }

      _currentPositionIndex--;
      _currentPositionIndex = Mathf.Clamp(_currentPositionIndex, 0, _availableMovePositions.Count - 1);
      MoveToPoint(_currentPositionIndex);
    }

    private void MoveToPoint(int pointIndex) {
      _moveTween?.Kill();
      _moveTween = _playerViewProvider.CurrentView.transform.DOMoveX(_availableMovePositions[pointIndex].x, _playerConfigurations.StepMoveSpeed);
    }

    private bool IsMoving {
      get {
        return _moveTween.IsActive() && _moveTween.IsPlaying();
      }
    }
  }
}
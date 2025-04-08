using System;

namespace Project.Scripts.Architecture.CodeBase.GameStates {
  public class Transition {
    private Action _onTransition;
    private readonly Func<bool>[] _conditions;

    public Transition(State firstState, State nextState, Action onTransition, params Func<bool>[] conditions) {
      FirstState = firstState;
      NextState = nextState;
      _onTransition = onTransition;
      _conditions = conditions;
    }

    public bool CanTransit() {
      foreach (Func<bool> condition in _conditions) {
        if (!condition()) {
          return false;
        }
      }

      return true;
    }

    public void TransitAction() { }
    public State FirstState { get; }
    public State NextState { get; }
  }
}
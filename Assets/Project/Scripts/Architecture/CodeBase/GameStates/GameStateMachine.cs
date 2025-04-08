using System;
using System.Collections.Generic;
using Project.Scripts.Architecture.CodeBase.GameStates.States;
using Project.Scripts.Architecture.CodeBase.Services.Events;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.GameStates {
  public class GameStateMachine: IGameStateMachine{
     private readonly Dictionary<Type, IExitState> _stateMap;
    private readonly IMonoEventsService _monoEvent;
    private IState _activeState;

    public GameStateMachine(DiContainer container) {
      _activeState = new NullState();

      _monoEvent = container.Resolve<IMonoEventsService>();

      State [] states = {
        container.Instantiate<LoadProgressState>(),
        container.Instantiate<LoadSceneState>(),
        /*container.Instantiate<LoadSceneState>(),
        container.Instantiate<WarmupFactoryState>(),
        container.Instantiate<SpawnMonoServiceState>(),
        container.Instantiate<InitUIManagerState>(),
        container.Instantiate<MenuState>()*/
      };

      for (var i = 0; i < states.Length - 1; i++) {
        State state = states[i];
        State nextState = states[i + 1];
        state.AddTransition(nextState, null, () => !state.IsActive);
      }

      _stateMap = new Dictionary<Type, IExitState>();

      foreach (State state in states) {
        _stateMap.Add(state.GetType(), state);
      }

      _monoEvent.OnUpdate += Update;
      _monoEvent.OnFixedUpdate += PhysicsUpdate;

      Enter<LoadProgressState>();
    }

    ~GameStateMachine() {
      _monoEvent.OnUpdate -= Update;
      _monoEvent.OnFixedUpdate -= PhysicsUpdate;
    }

    public void Update(object sender, EventArgs e) {
      _activeState.Update();
      CheckTransitions();
    }

    public void Enter<TState>() where TState : class, IState {
      ChangeState<TState>().Enter();
    }

    public void PhysicsUpdate(object sender, EventArgs e) {
      _activeState.PhysicsUpdate();
    }

    private void CheckTransitions() {
      Transition transition = _activeState.GetAvailableTransitions();

      if (transition != null) {
        ChangeState(transition.NextState);
      }
    }

    private TState ChangeState<TState>() where TState : class, IState {
      _activeState.Exit();
      var state = GetState<TState>();
      _activeState = state;
      return state;
    }

    private void ChangeState<TState>(TState nextState) where TState : class, IState {
      _activeState.Exit();
      TState state = nextState;
      _activeState = state;
      _activeState.Enter();
    }

    private TState GetState<TState>() where TState : class, IState {
      return _stateMap[typeof(TState)] as TState;
    }
  }
}
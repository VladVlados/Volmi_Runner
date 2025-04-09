using System;
using System.Collections.Generic;
using Project.Scripts.Architecture.CodeBase.GameStates.States;
using Project.Scripts.Architecture.CodeBase.GameStates.States.GameSetupStates;
using Project.Scripts.Architecture.CodeBase.GameStates.States.Lobby;
using Project.Scripts.Architecture.CodeBase.Services.Events;
using Project.Scripts.Architecture.CodeBase.Signal;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.GameStates {
  public class GameStateMachine : IGameStateMachine {
    private readonly Dictionary<Type, IExitState> _stateMap;
    private readonly IMonoEventsService _monoEvent;
    private readonly SignalBus _signalBus;
    
    private DiContainer _container;

    private IState _activeState;

    public GameStateMachine(DiContainer container, SignalBus signalBus) {
      _container = container;
      _signalBus = signalBus;
      _activeState = _container.Instantiate<NullState>();
      _signalBus.Subscribe<SceneReadySignal>(OnSceneReady);

      _monoEvent = _container.Resolve<IMonoEventsService>();

      State[] loadingStates = {
        _container.Instantiate<LoadProgressState>(),
        _container.Instantiate<InitUIManagerState>(),
        _container.Instantiate<LobbyLoadingState>()
      };

      foreach (State state in loadingStates) {
        state.Setup(this);
        state.UpdateContainer(container);
      }

      for (var i = 0; i < loadingStates.Length - 1; i++) {
        State state = loadingStates[i];
        State nextState = loadingStates[i + 1];
        state.AddTransition(nextState, null, () => !state.IsActive);
      }

      _stateMap = new Dictionary<Type, IExitState>();

      foreach (State state in loadingStates) {
        _stateMap.Add(state.GetType(), state);
      }

      _monoEvent.OnUpdate += Update;
      _monoEvent.OnFixedUpdate += PhysicsUpdate;

      Enter<LoadProgressState>();
    }

    ~GameStateMachine() {
      _monoEvent.OnUpdate -= Update;
      _monoEvent.OnFixedUpdate -= PhysicsUpdate;
      _signalBus.Unsubscribe<SceneReadySignal>(OnSceneReady);
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

    private void OnSceneReady(SceneReadySignal signal) {
      _container = signal.SceneContainer;

      foreach (KeyValuePair<Type, IExitState> state in _stateMap) {
        if (state.Value is State concreteState) {
          concreteState.UpdateContainer(_container);
        }
      }
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
      if (_stateMap.ContainsKey(typeof(TState))) {
        return _stateMap[typeof(TState)] as TState;
      }

      return CreateState<TState>();
    }

    private TState CreateState<TState>() where TState : class, IState {
      var state = _container.Instantiate<TState>();
      state.Setup(this);
      state.UpdateContainer(_container);
      return state;
    }
  }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Architecture.CodeBase.GameStates {
  public class State : IState {
    protected DiContainer _container;
    private readonly List<Transition> _transitions = new();
    protected IGameStateMachine _gameStateMachine;
    
    public void Setup(IGameStateMachine gameStateMachine) {
      _gameStateMachine = gameStateMachine;
    }

    public void UpdateContainer(DiContainer container) {
      _container = container;
    }
    
    public void Update() { }

    public Transition GetAvailableTransitions() {
      foreach (Transition transition in _transitions) {
        if (transition.CanTransit()) {
          return transition;
        }
      }

      return null;
    }

    public bool IsActive { get; protected set; }

    public virtual void Enter() {
      IsActive = true;
    }

    public void PhysicsUpdate() { }

    public void AddTransition(State nextState, Action action, params Func<bool>[] conditions) {
      _transitions.Add(new Transition(this, nextState, action, conditions));
    }

    public virtual void Exit() {
      IsActive = false;
    }
  }
}
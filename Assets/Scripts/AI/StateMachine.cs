using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMachine
{
    [System.Serializable]
    private class Transition
    {
        public Func<bool> CanTransition { get; }
        public IState NextState { get; }

        public Transition(IState nextState, Func<bool> transitionChecker)
        {
            NextState = nextState;
            CanTransition = transitionChecker;
        }
    }

    private IState _currentState = null;

    [SerializeField] private Dictionary<Type, List<Transition>> _allTransitions = new Dictionary<Type, List<Transition>>();
    [SerializeField] private List<Transition> _currentTransitions = new List<Transition>();
    [SerializeField] private List<Transition> _fromAnyTransitions = new List<Transition>();
    [SerializeField] private static List<Transition> EmptyTransition = new List<Transition>(0);

    public string CurrentStateName { get => (_currentState == null) ? "" : _currentState.GetType().ToString(); }

    public void Tick()
    {
        Transition executedTransition = GetExecutedTransition();
        if (executedTransition != null)
        {
            SetState(executedTransition.NextState);
        }

        _currentState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == _currentState)
        {
            return;
        }

        _currentState?.OnExit();
        _currentState = state;

        _allTransitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        
        if (_currentTransitions == null)
        {
            _currentTransitions = EmptyTransition;
        }

        _currentState.OnEnter();
    }

    private Transition GetExecutedTransition()
    {
        foreach (Transition transition in _fromAnyTransitions)
        {
            if (transition.CanTransition())
            {
                return transition;
            }
        }

        foreach(Transition transition in _currentTransitions)
        {
            if (transition.CanTransition())
            {
                return transition;
            }
        }

        return null;
    }

    public void AddTransition(IState fromState, IState toState, Func<bool> transitionChecker)
    {
        Type fromStateType = fromState.GetType();

        if (!_allTransitions.TryGetValue(fromStateType, out List<Transition> transitions))
        {
            transitions = new List<Transition>();
            _allTransitions[fromStateType] = transitions;
        }

        transitions.Add(new Transition(toState, transitionChecker));
    }

    public void AddAnyTransitions(IState targetState, Func<bool> transitionChecker)
    {
        _fromAnyTransitions.Add(new Transition(targetState, transitionChecker));
    }
}

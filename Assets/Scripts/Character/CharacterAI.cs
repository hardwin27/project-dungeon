using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAi : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterVisual _characterVisual;
    [SerializeField] private CharacterCombat _characterCombat;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        InitiateStateMachine();
    }

    private void Update() => _stateMachine.Tick();

    private void InitiateStateMachine()
    {
        _stateMachine = new StateMachine();
    }
}

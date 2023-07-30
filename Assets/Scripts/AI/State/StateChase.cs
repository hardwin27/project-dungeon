using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase : IState
{
    private CharacterMovement _characterMovement;
    private CharacterVisual _characterVisual;
    private CharacterAiData _aiData;

    public StateChase(CharacterMovement characterMovement, CharacterVisual characterVisual, CharacterAiData aiData)
    {
        _characterMovement = characterMovement;
        _characterVisual = characterVisual;
        _characterVisual = characterVisual;
        _aiData = aiData;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        if (_aiData.AiTarget == null)
        {
            return;
        }

        Transform targetTransform = _aiData.AiTarget.transform;

        _characterVisual.LookTo(targetTransform.position);
        _characterMovement.MoveToDirection((targetTransform.position - _characterVisual.transform.position).normalized);
    }
}

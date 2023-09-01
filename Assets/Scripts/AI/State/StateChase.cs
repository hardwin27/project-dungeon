using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase : IState
{
    private CharacterMovement _characterMovement;
    private CharacterVisual _characterVisual;
    private AiData _aiData;

    public StateChase(CharacterMovement characterMovement, CharacterVisual characterVisual, AiData aiData)
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
        if (!_aiData.IsTargetDetected)
        {
            return;
        }

        Transform targetTransform = _aiData.AiTargets[0].transform;

        _characterVisual.LookTo(targetTransform.position);
        _characterMovement.MoveToDirection((targetTransform.position - _characterVisual.transform.position).normalized);
    }
}

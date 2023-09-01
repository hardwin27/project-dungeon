using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase : IState
{
    private CharacterMovement _characterMovement;
    private CharacterVisual _characterVisual;
    private AiData _aiData;
    private float _sqrChaseDistanceMargin;

    public StateChase(AiData aiData, CharacterMovement characterMovement, CharacterVisual characterVisual, float chaseDistanceMargin)
    {
        _aiData = aiData;
        _characterMovement = characterMovement;
        _characterVisual = characterVisual;
        _sqrChaseDistanceMargin = chaseDistanceMargin * chaseDistanceMargin;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        ChaseHandler();
    }

    private void ChaseHandler()
    {
        if (!_aiData.IsTargetDetected)
        {
            return;
        }

        _characterVisual.LookTo(_aiData.AiTargets[0].transform.position);

        Vector2 vectorToTarget = (Vector2)(_aiData.AiTargets[0].transform.position - _characterMovement.transform.position);

        if (Mathf.Abs(vectorToTarget.sqrMagnitude) >= _sqrChaseDistanceMargin)
        {
            _characterMovement.MoveToDirection(vectorToTarget.normalized);
        }
        else
        {
            _characterMovement.MoveToDirection(Vector2.zero);
        }

    }
}

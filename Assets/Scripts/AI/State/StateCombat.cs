using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCombat : IState
{
    private CharacterMovement _characterMovement;
    private CharacterVisual _characterVisual;
    private CharacterCombat _characterCombat;
    private AiData _aiData;
    
    public StateCombat(CharacterMovement characterMovement, CharacterVisual characterVisual, CharacterCombat characterCombat, AiData aiData)
    {
        _characterMovement = characterMovement;
        _characterVisual = characterVisual;
        _characterCombat = characterCombat;
        _aiData = aiData;
    }

    public void OnEnter()
    {
        _characterMovement.MoveToDirection(Vector2.zero);
    }

    public void OnExit()
    {
        _characterCombat.EndInputAction();
    }

    public void Tick()
    {
        if (_aiData.IsTargetDetected)
        {
            _characterVisual.LookTo(_aiData.AiTargets[0].transform.position);
            _characterCombat.StartInputAction();
        }
    }
}

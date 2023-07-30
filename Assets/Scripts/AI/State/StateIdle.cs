using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : IState
{
    private CharacterMovement _characterMovement;
    private CharacterVisual _characterVisual;

    private float _minLookAroundInterval = 1f;
    private float _maxLookAroundInterval = 5f;
    private float _lookAroundInterval;
    private float _lookAroundTimer;

    public StateIdle(CharacterMovement characterMovement, CharacterVisual characterVisual)
    {
        _characterMovement = characterMovement;
        _characterVisual = characterVisual;
    }

    public void OnEnter()
    {
        _characterMovement.MoveToDirection(Vector2.zero);
        _characterVisual.LookTo(GetRandomPositionAround());

        RestartLookAroundTimer();
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        if (_lookAroundTimer > 0f)
        {
            _lookAroundTimer -= Time.deltaTime;
        }
        else
        {
            _characterVisual.LookTo(GetRandomPositionAround());
            RestartLookAroundTimer();
        }
    }

    private Vector3 GetRandomPositionAround()
    {
        return _characterVisual.transform.position + new Vector3 (Random.Range(0f, 1f), Random.Range(0f, 1f) + 0f);
    }

    private void RestartLookAroundTimer()
    {
        _lookAroundInterval = Random.Range(_minLookAroundInterval, _maxLookAroundInterval);
    }
}

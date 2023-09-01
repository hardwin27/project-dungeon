using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAiBrain_Follower : CharacterAiBrain
{
    [Space(5)]
    [Header("State Chase")]
    protected StateChase _stateChase;
    [SerializeField] private float _chaseDistanceMargin;

    protected override void InitiateStateMachine()
    {
        base.InitiateStateMachine();

        _stateChase = new StateChase(_aiData, _characterMovement, _characterVisual, _chaseDistanceMargin);

        _stateMachine.AddTransition(_stateIdle, _stateChase, () => _aiData.IsTargetDetected);
        _stateMachine.AddTransition(_stateChase, _stateIdle, () => !_aiData.IsTargetDetected);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_characterVisual.transform.position, _chaseDistanceMargin);
    }
}

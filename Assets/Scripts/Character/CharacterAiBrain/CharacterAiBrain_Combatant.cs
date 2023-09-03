using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAiBrain_Combatant : CharacterAiBrain
{
    [SerializeField] protected CharacterCombat _characterCombat;

    [Space(5)]
    [Header("State Chase")]
    protected StateChase _stateChase;
    [SerializeField] private float _chaseDistanceMargin;

    [Space(5)]
    [Header("StateCombat")]
    protected StateCombat _stateCombat;
    [SerializeField] protected bool _canAttack;
    [SerializeField] protected float _combatDetectionRange;

    protected override void InitiateStateMachine()
    {
        base.InitiateStateMachine();

        _stateChase = new StateChase(_aiData, _characterMovement, _characterVisual, _chaseDistanceMargin);
        _stateCombat = new StateCombat(_aiData, _characterMovement, _characterVisual, _characterCombat);

        _stateMachine.AddTransition(_stateIdle, _stateChase, () => _aiData.IsTargetDetected);
        _stateMachine.AddTransition(_stateChase, _stateIdle, () => !_aiData.IsTargetDetected);

        _stateMachine.AddTransition(_stateIdle, _stateCombat, () => _canAttack);
        _stateMachine.AddTransition(_stateChase, _stateCombat, () => _canAttack);

        _stateMachine.AddTransition(_stateCombat, _stateIdle, () => !_canAttack && !_aiData.IsTargetDetected);
        _stateMachine.AddTransition(_stateCombat, _stateChase, () => !_canAttack && _aiData.IsTargetDetected);
    }

    protected override void DetectorHandler()
    {
        base.DetectorHandler();
        CanAttackHandler();
    }

    protected void CanAttackHandler()
    {
        if (!_aiData.IsTargetDetected)
        {
            _canAttack = false;
        }
        else
        {
            Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(_characterVisual.transform.position, _combatDetectionRange, _aiData.TargetLayer);
            foreach (Collider2D overlapCollider in overlapColliders)
            {
                if (overlapCollider.gameObject == _aiData.AiTargets[0])
                {
                    _canAttack = true;
                    return;
                }
            }

            _canAttack = false;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(_characterVisual.transform.position, _chaseDistanceMargin);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_characterVisual.transform.position, _combatDetectionRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAiBrain : MonoBehaviour
{
    [SerializeField] protected CharacterMovement _characterMovement;
    [SerializeField] protected CharacterVisual _characterVisual;
    /*[SerializeField] protected CharacterCombat _characterCombat;*/

    [SerializeField] protected AiData _aiData = new AiData();

    [SerializeField] protected float _targetDetectionRange;
    /*[SerializeField] protected bool _canAttack;
    [SerializeField] protected float _combatDetectionRange;*/

    [SerializeField] protected string _currentState;
    
    [SerializeField] protected StateMachine _stateMachine;

    protected StateIdle _stateIdle;

    protected virtual void Awake()
    {
        InitiateStateMachine();
        _stateMachine.SetState(_stateIdle);
    }

    protected virtual void Update()
    {
        DetectorHandler();

        _stateMachine.Tick();

        _currentState = _stateMachine.CurrentStateName;
    }

    protected virtual void InitiateStateMachine()
    {
        _stateMachine = new StateMachine();

        _stateIdle = new StateIdle(_characterMovement, _characterVisual);
        /*StateChase stateChase = new StateChase(_characterMovement, _characterVisual, _aiData);
        StateCombat stateCombat = new StateCombat(_characterMovement, _characterVisual, _characterCombat, _aiData);

        _stateMachine.AddTransition(stateIdle, stateChase, () => _aiData.IsTargetDetected);
        _stateMachine.AddTransition(stateChase, stateIdle, () => !_aiData.IsTargetDetected);

        _stateMachine.AddTransition(stateIdle, stateCombat, () => _canAttack);
        _stateMachine.AddTransition(stateChase, stateCombat, () => _canAttack);

        _stateMachine.AddTransition(stateCombat, stateIdle, () => !_canAttack && !_aiData.IsTargetDetected);
        _stateMachine.AddTransition(stateCombat, stateChase, () => !_canAttack && _aiData.IsTargetDetected);*/
    }

    protected virtual void DetectorHandler()
    {
        DetectTarget();
        /*CanAttackHandler();*/
    }

    protected void DetectTarget()
    {
        List<GameObject> detectedTargets = new List<GameObject>();
        Collider2D[] detectedColliders = Physics2D.OverlapCircleAll(transform.position, _targetDetectionRange, _aiData.TargetLayer);

        foreach (Collider2D detectedCollider in detectedColliders)
        {
            if (_aiData.TargetTags.Contains(detectedCollider.gameObject.tag))
            {
                detectedTargets.Add(detectedCollider.gameObject);
            }
        }


        _aiData.AiTargets = detectedTargets;
    }

    /*protected void CanAttackHandler()
    {
        if (!_aiData.IsTargetDetected)
        {
            _canAttack = false;
        }
        else
        {
            Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(_characterVisual.transform.position, _combatDetectionRange, _aiData.TargetLayer);
            foreach(Collider2D overlapCollider in overlapColliders)
            {
                if (overlapCollider.gameObject == _aiData.AiTargets[0])
                {
                    _canAttack = true;
                    return;
                }
            }

            _canAttack = false;
        }
    }*/

    protected virtual void OnDrawGizmos()
    {
        if (_characterVisual == null)
        {
            return;
        }

        AiTargetDetectionGizmos();
        /*AiCombatRangeGizmos();*/
    }

    protected void AiTargetDetectionGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_characterVisual.transform.position, _targetDetectionRange);
    }

    /*protected void AiCombatRangeGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_characterVisual.transform.position, _combatDetectionRange);
    }*/
}

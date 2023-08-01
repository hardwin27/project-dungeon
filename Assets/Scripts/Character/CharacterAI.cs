using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAiData
{
    [SerializeField] private GameObject _aiTarget = null;
    [SerializeField] private bool _canAttack = false;

    public GameObject AiTarget { set => _aiTarget = value; get => _aiTarget; }
    public bool CanAttack { set => _canAttack = value; get => _canAttack; }
}

public class CharacterAi : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterVisual _characterVisual;
    [SerializeField] private CharacterCombat _characterCombat;

    [SerializeField] private CharacterAiData _aiData = new CharacterAiData();

    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private float _targetDetectionRange;

    [SerializeField] private float _combatDetectionRange;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        InitiateStateMachine();
    }

    private void Update()
    {
        DetectorHandler();

        _stateMachine.Tick();
    }

    private void InitiateStateMachine()
    {
        _stateMachine = new StateMachine();

        StateIdle stateIdle = new StateIdle(_characterMovement, _characterVisual);
        StateChase stateChase = new StateChase(_characterMovement, _characterVisual, _aiData);
        StateCombat stateCombat = new StateCombat(_characterMovement, _characterVisual, _characterCombat, _aiData);

        _stateMachine.SetState(stateIdle);

        _stateMachine.AddTransition(stateIdle, stateChase, () => _aiData.AiTarget != null);
        _stateMachine.AddTransition(stateChase, stateIdle, () => _aiData.AiTarget == null);

        _stateMachine.AddTransition(stateIdle, stateCombat, () => _aiData.CanAttack);
        _stateMachine.AddTransition(stateChase, stateCombat, () => _aiData.CanAttack);

        _stateMachine.AddTransition(stateCombat, stateIdle, () => !_aiData.CanAttack && _aiData.AiTarget == null);
        _stateMachine.AddTransition(stateCombat, stateChase, () => !_aiData.CanAttack && _aiData.AiTarget != null);
    }

    private void DetectorHandler()
    {
        DetectTarget();
        CanAttackHandler();
    }

    private void DetectTarget()
    {
        Collider2D targetCollider = Physics2D.OverlapCircle(_characterVisual.transform.position, _targetDetectionRange, _targetLayerMask);
        if (targetCollider != null)
        {
            _aiData.AiTarget = targetCollider.gameObject;
        }
        else
        {
            _aiData.AiTarget = null;
        }
    }

    private void CanAttackHandler()
    {
        if (_aiData.AiTarget == null)
        {
            _aiData.CanAttack = false;
        }
        else
        {
            Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(_characterVisual.transform.position, _combatDetectionRange, _targetLayerMask);
            foreach(Collider2D overlapCollider in overlapColliders)
            {
                if (overlapCollider.gameObject == _aiData.AiTarget)
                {
                    _aiData.CanAttack = true;
                    return;
                }
            }

            _aiData.CanAttack = false;
        }

        /*Collider2D targetCollider = Physics2D.OverlapCircle(_characterVisual.transform.position, _combatDetectionRange, _targetLayerMask);
        
        if (targetCollider != null)
        {
            _aiData.AiTarget = targetCollider.gameObject;
        }
        else
        {
            _aiData.AiTarget = null;
        }*/
    }

    private void OnDrawGizmos()
    {
        if (_characterVisual == null)
        {
            return;
        }

        AiTargetDetectionGizmos();
        AiCombatRangeGizmos();
    }

    private void AiTargetDetectionGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_characterVisual.transform.position, _targetDetectionRange);
    }

    private void AiCombatRangeGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_characterVisual.transform.position, _combatDetectionRange);
    }
}

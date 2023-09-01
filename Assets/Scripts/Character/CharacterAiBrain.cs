using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAiBrain : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterVisual _characterVisual;
    [SerializeField] private CharacterCombat _characterCombat;

    [SerializeField] private AiData _aiData = new AiData();

    [SerializeField] private float _targetDetectionRange;

    [SerializeField] private float _combatDetectionRange;

    [SerializeField] private string _currentState;
    
    [SerializeField] private StateMachine _stateMachine;

    private void Awake()
    {
        InitiateStateMachine();
    }

    private void Update()
    {
        DetectorHandler();

        _stateMachine.Tick();

        _currentState = _stateMachine.CurrentStateName;
    }

    private void InitiateStateMachine()
    {
        _stateMachine = new StateMachine();

        StateIdle stateIdle = new StateIdle(_characterMovement, _characterVisual);
        StateChase stateChase = new StateChase(_characterMovement, _characterVisual, _aiData);
        StateCombat stateCombat = new StateCombat(_characterMovement, _characterVisual, _characterCombat, _aiData);

        _stateMachine.AddTransition(stateIdle, stateChase, () => _aiData.IsTargetDetected);
        _stateMachine.AddTransition(stateChase, stateIdle, () => !_aiData.IsTargetDetected);

        _stateMachine.AddTransition(stateIdle, stateCombat, () => _aiData.CanAttack);
        _stateMachine.AddTransition(stateChase, stateCombat, () => _aiData.CanAttack);

        _stateMachine.AddTransition(stateCombat, stateIdle, () => !_aiData.CanAttack && !_aiData.IsTargetDetected);
        _stateMachine.AddTransition(stateCombat, stateChase, () => !_aiData.CanAttack && _aiData.IsTargetDetected);



        _stateMachine.SetState(stateIdle);
    }

    private void DetectorHandler()
    {
        DetectTarget();
        CanAttackHandler();
    }

    private void DetectTarget()
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

    private void CanAttackHandler()
    {
        if (!_aiData.IsTargetDetected)
        {
            _aiData.CanAttack = false;
        }
        else
        {
            Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(_characterVisual.transform.position, _combatDetectionRange, _aiData.TargetLayer);
            foreach(Collider2D overlapCollider in overlapColliders)
            {
                if (overlapCollider.gameObject == _aiData.AiTargets[0])
                {
                    _aiData.CanAttack = true;
                    return;
                }
            }

            _aiData.CanAttack = false;
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterAiData
{
    [SerializeField] private GameObject _aiTarget = null;

    public GameObject AiTarget { set => _aiTarget = value; get => _aiTarget; }
}

public class CharacterAi : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterVisual _characterVisual;
    [SerializeField] private CharacterCombat _characterCombat;

    [SerializeField] private CharacterAiData _aiData = new CharacterAiData();

    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private float _targetDetectionRange;
    
    private StateMachine _stateMachine;

    private void Awake()
    {
        InitiateStateMachine();
    }

    private void Update()
    {
        DetectTarget();

        _stateMachine.Tick();
    }

    private void InitiateStateMachine()
    {
        _stateMachine = new StateMachine();
    }

    private void DetectTarget()
    {
        Collider2D targetCollider = Physics2D.OverlapCircle(_characterVisual.transform.position, _targetDetectionRange, _targetLayerMask);
        if (targetCollider != null)
        {
            _aiData.AiTarget = (targetCollider.attachedRigidbody == null) ? targetCollider.gameObject : targetCollider.attachedRigidbody.gameObject;
        }
        else
        {
            _aiData.AiTarget = null;
        }
    }
}

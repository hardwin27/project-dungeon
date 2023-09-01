using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AiData
{
    [SerializeField] private List<GameObject> _aiTargets = new List<GameObject>();
    [SerializeField] private bool _canAttack = false;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private List<string> _targetTags = new List<string>();

    public List<GameObject> AiTargets { set => _aiTargets = value; get => _aiTargets; }

    public bool IsTargetDetected { get => (_aiTargets.Count > 0); }
    public bool CanAttack { set => _canAttack = value; get => _canAttack; }
    public LayerMask TargetLayer { set => _targetLayer = value; get => _targetLayer; }
    public List<string> TargetTags { set => _targetTags = value; get => _targetTags; }
}

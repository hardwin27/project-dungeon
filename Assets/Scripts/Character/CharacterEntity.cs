using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : MonoBehaviour, IHurtResponder
{
    [SerializeField] private float _health;

    public GameObject Owner => gameObject;
    [SerializeField] private IHurtArea[] _hurtAreas;

    private void Awake()
    {
        InitiateHurtBoxes();
    }

    private void InitiateHurtBoxes()
    {
        _hurtAreas = GetComponentsInChildren<IHurtArea>();

        foreach (IHurtArea hurtArea in _hurtAreas)
        {
            hurtArea.HurtResponder = this;
        }
    }

    public bool CheckHit(HitData hitData)
    {
        throw new System.NotImplementedException();
    }

    public void Response(HitData hitData)
    {
        throw new System.NotImplementedException();
    }
}

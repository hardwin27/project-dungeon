using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : MonoBehaviour, IHurtResponder, IHaveHealth, IHaveTeam
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Team _myTeam;


    public GameObject Owner => gameObject;

    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public Team MyTeam { get => _myTeam; set => _myTeam = value; }

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

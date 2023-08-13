using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : MonoBehaviour, IHurtResponder, IHaveHealth, IHaveTeam
{

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private Team _myTeam;


    public GameObject Owner => gameObject;


    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public Team MyTeam { get => _myTeam; set => _myTeam = value; }

    [SerializeField] private IHurtArea[] _hurtAreas;

    private void Awake()
    {
        InitiateHurtBoxes();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
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
        return true;
    }

    public void Response(HitData hitData)
    {
        AddHealth(-hitData.Damage);
    }

    private void AddHealth(float healthValue)
    {
        CurrentHealth += healthValue;

        if (CurrentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }
}

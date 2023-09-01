using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : MonoBehaviour, IHurtResponder, IHaveHealth
{

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool _isAlive = true;

    public GameObject Owner => gameObject;


    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public bool IsAlive { get => _isAlive; set => _isAlive = value; }

    [SerializeField] private IHurtArea[] _hurtAreas;

    private void Awake()
    {
        InitiateHurtBoxes();
    }

    private void OnEnable()
    {
        IsAlive = true;
        CurrentHealth = MaxHealth;
    }

    private void Start()
    {
        
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
        return IsAlive;
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
            Die();
        }
    }

    private void Die()
    {
        _isAlive = false;
        gameObject.SetActive(false);
        Destroy(gameObject, 3f);
    }
}

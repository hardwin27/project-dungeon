using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    [SerializeField] private CharacterEntity _characterEntity;

    [SerializeField] private Weapon _weapon;
    /*[SerializeField] private float _damage;*/
    [SerializeField] private float _damageMultiplier = 0f;

    [SerializeField] private List<string> _hostileTags = new List<string>();

    /*public float Damage => _damage;*/

    public GameObject Owner => gameObject;

    public float DamageMultiplier 
    { 
        get => _damageMultiplier; 
        set => _damageMultiplier = value; 
    }

    private void Start()
    {
        /*_weapon.OwnerHitResponder = this;*/
        _weapon.OwnerEntity = _characterEntity;
    }

    public void StartInputAction()
    {
        _weapon.ActionInputStart();
    }

    public void EndInputAction()
    {
        _weapon.ActionInputEnd();
    }
}

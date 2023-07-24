using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour, IHitResponder
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _damage;

    public float Damage => _damage;

    public GameObject Owner => gameObject;

    private void Start()
    {
        _weapon.OwnerHitResponder = this;
    }

    public void StartInputAction()
    {
        Debug.Log("StartInputAction");
        _weapon.ActionInputStart();
    }

    public void EndInputAction()
    {
        Debug.Log("EndInputAction");
        _weapon.ActionInputEnd();
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

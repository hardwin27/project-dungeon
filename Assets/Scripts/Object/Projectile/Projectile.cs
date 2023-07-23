using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _projectileDamage;
    [SerializeField] private Rigidbody2D _body;

    private IHitArea _hitArea;

    public Rigidbody2D Body { get { return _body; } }
    public IHitArea HitArea { get { return _hitArea; } }
    /*public IHitResponder OwnerHitResponder { set; get; }*/

    private void OnEnable()
    {
        _hitArea = GetComponent<IHitArea>();
    }

    public float ProjectileDamage
    {
        set { _projectileDamage = value; }
        get { return _projectileDamage; }
    }

    public float Damage => _projectileDamage;
}

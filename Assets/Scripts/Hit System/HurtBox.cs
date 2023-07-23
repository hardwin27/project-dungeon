using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBox : MonoBehaviour, IHurtArea
{
    [SerializeField] private bool _active;
    /*[SerializeField] private GameObject _owner;*/
    private IHurtResponder _hurtResponder;
    private Collider2D _hurtCollider;

    public bool Active { get { return _active; } }

    /*public GameObject Owner { get { return _owner; } }*/

    public Transform HurtAreaTransform { get { return transform; } }

    public Collider2D HurtCollider { get { return _hurtCollider; } }

    public IHurtResponder HurtResponder { get { return _hurtResponder; } set { _hurtResponder = value; } }

    public bool CheckHit(HitData hitData)
    {
        return true;
    }

    private void OnEnable()
    {
        _hurtCollider = GetComponent<Collider2D>();
    }
}

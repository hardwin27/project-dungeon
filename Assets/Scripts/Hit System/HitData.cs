using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitData
{
    [SerializeField] private float _damage;
    [SerializeField] private Vector3 _hitPoint;
    [SerializeField] private IHurtArea _hurtArea;
    [SerializeField] private IHitArea _hitArea;

    public float Damage
    {
        set { _damage = value; }
        get { return _damage; }
    }

    public Vector3 HitPoint
    {
        set { _hitPoint = value; }
        get { return _hitPoint; }
    }

    public IHurtArea HurtArea
    {
        set { _hurtArea = value; }
        get { return _hurtArea; }
    }

    public IHitArea HitArea
    {
        set { _hitArea = value; }
        get { return _hitArea; }
    }

    public bool Validate()
    {
        if (HurtArea != null)
        {
            if (HurtArea.CheckHit(this))
            {
                if (HurtArea.HurtResponder == null || HurtArea.HurtResponder.CheckHit(this))
                {
                    if (HitArea.HitResponder == null || HitArea.HitResponder.CheckHit(this))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}

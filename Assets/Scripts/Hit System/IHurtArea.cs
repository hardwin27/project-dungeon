using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHurtArea
{
    public bool Active { get; }
    /*public GameObject Owner { get; }*/
    public Transform HurtAreaTransform { get; }
    public Collider2D HurtCollider { get; }
    public IHurtResponder HurtResponder { set; get; }

    public bool CheckHit(HitData hitData);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitArea
{
    public Transform HitAreaTransform { get; }
    public IHitResponder HitResponder { set; get; }
    public void CheckHit(IHurtArea hurtArea);
}

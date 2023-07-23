using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitResponder
{
    public float Damage { get; }

    public GameObject Owner { get; }

    public bool CheckHit(HitData hitData);
    public void Response(HitData hitData);
}

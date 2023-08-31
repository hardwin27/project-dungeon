using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveHealth 
{
    public float MaxHealth { set; get; }
    public float CurrentHealth { set; get; }

    public bool IsAlive { set; get; }
}

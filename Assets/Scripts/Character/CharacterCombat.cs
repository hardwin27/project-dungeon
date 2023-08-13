using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour, IHitResponder
{
    [SerializeField] private CharacterEntity _characterEntity;

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
        _weapon.ActionInputStart();
    }

    public void EndInputAction()
    {
        _weapon.ActionInputEnd();
    }

    public bool CheckHit(HitData hitData)
    {
        if (hitData.HurtArea == null)
        {
            return false;
        }

        if (hitData.HurtArea.HurtResponder.Owner == Owner)
        {
            return false;
        }

        if(hitData.HurtArea.HurtResponder.Owner.TryGetComponent(out IHaveTeam haveTeamCom))
        {
            if (haveTeamCom.MyTeam == _characterEntity.MyTeam)
            {
                return false;
            }
        }

        return true;
    }

    public void Response(HitData hitData)
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IHitResponder
{
    [SerializeField] private CharacterEntity _ownerEntity;

    [SerializeField] protected bool _isOnAction = false;
    [SerializeField] protected float _actionCooldown;
    [SerializeField] protected float _damage;
    protected float _cooldownTimer = -1f;

    public bool IsOnAction
    {
        get { return _isOnAction; }
    }

    public bool IsOnCooldown
    {
        get
        {
            return (_cooldownTimer > 0f);
        }
    }

    public CharacterEntity OwnerEntity
    {
        set => _ownerEntity = value;
        get => _ownerEntity;
    }

    public float Damage => _damage;

    public GameObject Owner
    {
        get => _ownerEntity?.gameObject;
    }

    protected virtual void Update()
    {
        CooldownTimerHandler();
    }

    protected virtual void CooldownTimerHandler()
    {
        if (IsOnCooldown)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    protected virtual void Action()
    {

    }

    protected virtual void StartCooldown()
    {
        _cooldownTimer = _actionCooldown;
    }

    public virtual void ActionInputStart()
    {
        if (CanDoAction())
        {
            Action();
        }
    }

    public virtual void ActionInputEnd()
    {

    }

    protected virtual bool CanDoAction()
    {
        return (!IsOnCooldown);
    }

    public bool CheckHit(HitData hitData)
    {
        if (_ownerEntity == null)
        {
            return false;
        }

        if (hitData.HurtArea == null)
        {
            return false;
        }

        if (hitData.HurtArea.HurtResponder.Owner == Owner)
        {
            return false;
        }

        if (hitData.HurtArea.HurtResponder.Owner.TryGetComponent(out IHaveTeam haveTeamCom))
        {
            if (haveTeamCom.MyTeam == _ownerEntity.MyTeam)
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

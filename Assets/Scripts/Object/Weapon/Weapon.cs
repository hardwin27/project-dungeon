using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected bool _isOnAction = false;
    [SerializeField] protected float _actionCooldown;
    private float _cooldownTimer = -1f;
    protected IHitResponder _ownerHitResponder;
    public IHitResponder OwnerHitResponder { set; get; }

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
}

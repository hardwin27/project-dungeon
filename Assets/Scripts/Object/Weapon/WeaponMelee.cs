using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : Weapon
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ObjectAnimationEventBroadcaster _weaponAnimationEventBroadcaster;

    protected List<IHitArea> _hitAreas = new List<IHitArea>();

    protected virtual void OnEnable()
    {
        if (_weaponAnimationEventBroadcaster != null)
        {
            _weaponAnimationEventBroadcaster.EventTriggered += AnimEventTriggered;
        }
    }

    protected virtual void OnDisable()
    {
        if (_weaponAnimationEventBroadcaster != null)
        {
            _weaponAnimationEventBroadcaster.EventTriggered -= AnimEventTriggered;
        }
    }

    protected virtual void Start()
    {
        AssignHitAreas();
    }

    protected override void Action()
    {
        base.Action();
        _isOnAction = true;
        _animator.SetTrigger("Attack");
    }

    protected void AssignHitAreas()
    {
        _hitAreas = new List<IHitArea>(GetComponentsInChildren<IHitArea>());
        foreach(IHitArea hitArea in _hitAreas)
        {
            Debug.Log($"Found {hitArea.HitAreaTransform.name} HitArea for {gameObject.name}");
            hitArea.HitResponder = this;
        }
    }

    private void AnimEventTriggered(string eventName)
    {
        switch(eventName)
        {
            case "AttackEnded":
                _isOnAction = false;
                StartCooldown();
                break;
        }
    }
}

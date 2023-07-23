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

    [SerializeField] protected Collider2D _pickUpTrigger;
    protected string _weaponLayerName = "Weapon";
    [SerializeField] private Vector3 _posOffsetWhenPicked;
    public Vector3 PosOffsetWhenPicked { get => _posOffsetWhenPicked; }

    protected virtual void Awake()
    {
        /*_pickUpTrigger = GetComponent<Collider2D>();*/
        _pickUpTrigger.enabled = true;
        gameObject.layer = LayerMask.NameToLayer(_weaponLayerName);


    }

    public virtual void WeaponPicked(int parentLayer)
    {
        _pickUpTrigger.enabled = false;
        gameObject.layer = parentLayer;
        _isOnAction = false;
        _cooldownTimer = -1;
        /*transform.position = pickerTransform.position + _posOffsetWhenPicked;
        transform.rotation = pickerTransform.rotation;*/
    }

    public virtual void WeaponDropped()
    {
        _pickUpTrigger.enabled = true;
        gameObject.layer = LayerMask.NameToLayer(_weaponLayerName);
        _isOnAction = false;
        _cooldownTimer = -1;
        /*transform.position = dropPosition;
        transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);*/
    }

    public virtual void WeaponEnabled()
    {
        gameObject.SetActive(true);

    }

    public virtual void WeaponDisabled()
    {
        gameObject.SetActive(false);
    }

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

    public IHitResponder OwnerHitResponder { set; get; }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _projectileDamage;
    [SerializeField] private Rigidbody2D _body;

    [SerializeField] private float _projectileDuration;

    private IHitArea _hitArea;

    public Rigidbody2D Body { get { return _body; } }
    public IHitArea HitArea { get { return _hitArea; } }
    /*public IHitResponder OwnerHitResponder { set; get; }*/

    private void OnEnable()
    {
        _hitArea = GetComponent<IHitArea>();
        if (_hitArea !=null )
        {
            _hitArea.OnHitted += OnHittedHandler;
        }
        StartCoroutine(DestroyProjectile(_projectileDuration));
    }

    private void OnDestroy()
    {
        if (_hitArea != null)
        {
            _hitArea.OnHitted -= OnHittedHandler;
        }
    }

    public float ProjectileDamage
    {
        set { _projectileDamage = value; }
        get { return _projectileDamage; }
    }

    public float Damage => _projectileDamage;

    private IEnumerator DestroyProjectile(float destroyDelay)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    private void OnHittedHandler()
    {
        StopCoroutine("DestroyProjectile");
        Destroy(gameObject);
    }
}

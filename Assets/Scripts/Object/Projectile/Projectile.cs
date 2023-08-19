using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _body;

    [SerializeField] protected float _projectileDuration;

    protected IHitArea _hitArea;

    public Rigidbody2D Body { get { return _body; } }
    public IHitArea HitArea { get { return _hitArea; } }/*public IHitResponder OwnerHitResponder { set; get; }*/

    protected void OnEnable()
    {
        _hitArea = GetComponent<IHitArea>();
        if (_hitArea !=null )
        {
            _hitArea.OnHitted += OnHittedHandler;
        }
        StartCoroutine(DestroyProjectile(_projectileDuration));
    }

    protected void OnDestroy()
    {
        if (_hitArea != null)
        {
            _hitArea.OnHitted -= OnHittedHandler;
        }
    }

    protected IEnumerator DestroyProjectile(float destroyDelay)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    protected virtual void OnHittedHandler()
    {
        StopCoroutine("DestroyProjectile");
        Destroy(gameObject);
    }
}

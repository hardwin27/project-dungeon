using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectileBasedSpread : WeaponProjectileBased
{
    [SerializeField] protected List<Transform> _spreadPoints;

    protected override void Shoot()
    {
        foreach(Transform spreadPoint in _spreadPoints)
        {
            GameObject projectileObject = Instantiate(_projectilePrefab, transform.position, transform.rotation);
            if (projectileObject.TryGetComponent(out Projectile projectile))
            {
                projectile.HitArea.HitResponder = OwnerHitResponder;
                projectile.Body.AddForce(_projectileForce * (spreadPoint.position - transform.position).normalized, ForceMode2D.Impulse);
                projectile.ProjectileDamage = OwnerHitResponder.Damage;
            }
        }
    }
}

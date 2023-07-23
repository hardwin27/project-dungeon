using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectileBased : Weapon
{
    [SerializeField] protected GameObject _projectilePrefab;
    [SerializeField] protected float _projectileForce;

    protected override void Action()
    {
        base.Action();
    }

    protected virtual void Shoot()
    {
        GameObject projectileObject = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        if (projectileObject.TryGetComponent(out Projectile projectile))
        {
            projectile.HitArea.HitResponder = OwnerHitResponder;
            projectile.Body.AddForce(_projectileForce * transform.right, ForceMode2D.Impulse);
            projectile.ProjectileDamage = OwnerHitResponder.Damage;
        }
    }
}

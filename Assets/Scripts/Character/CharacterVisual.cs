using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private Transform _rotatedTransform;

    [SerializeField] private Vector2 _lookDirection = Vector2.zero;

    private void Update()
    {
        LookDirectionHandler();
    }

    public void LookTo(Vector3 posToLook)
    {
        _lookDirection = posToLook - transform.position;
    }

    private void LookDirectionHandler()
    {
        if (_lookDirection == Vector2.zero)
        {
            return;
        }

        float lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
        _rotatedTransform.eulerAngles = new Vector3(_rotatedTransform.localRotation.x, _rotatedTransform.localRotation.y, lookAngle);

        _rotatedTransform.localScale = new Vector3(_rotatedTransform.localScale.x, Mathf.Sign(_lookDirection.x), _rotatedTransform.localScale.z);
    }
}

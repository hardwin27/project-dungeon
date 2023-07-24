using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Vector2 _cameraOffset = Vector2.zero;

    private void Update()
    {
        CameraFollowHandler();
    }

    private void CameraFollowHandler()
    {
        if (_playerTransform != null)
        {
            transform.position = new Vector3(_playerTransform.position.x + _cameraOffset.x, _playerTransform.position.y + _cameraOffset.y, transform.position.z);
        }
    }
}

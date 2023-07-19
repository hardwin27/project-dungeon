using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _body;

    private Vector2 _moveInput = Vector2.zero;

    [SerializeField] private float _moveForce;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minVelocity;
    private float _sqrMoveSpeed;

    private void Start()
    {
        _sqrMoveSpeed = _moveSpeed * _moveSpeed;
    }

    private void FixedUpdate()
    {
        MovementHandler();
    }

    public void MoveToDirection(Vector2 moveInput)
    {
        _moveInput = moveInput;
    }

    private void MovementHandler()
    {
        if (_body.velocity.sqrMagnitude < _sqrMoveSpeed)
        {
            _body.AddForce(_moveInput.normalized * _moveForce);
        }

        float xVelocity = _body.velocity.x;
        if (Mathf.Abs(xVelocity) < _minVelocity)
        {
            xVelocity = 0f;
        }
        float yVelocity = _body.velocity.y;
        if (Mathf.Abs(yVelocity) < _minVelocity)
        {
            yVelocity = 0f;
        }

        _body.velocity = new Vector2(xVelocity, yVelocity);
    }
}

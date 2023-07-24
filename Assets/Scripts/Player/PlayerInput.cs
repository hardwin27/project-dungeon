using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterCombat _characterCombat;

    private void Update()
    {
        InputDirectionHandler();
        InputActionHandler();
    }

    private void InputDirectionHandler()
    {
        _characterMovement.MoveToDirection(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void InputActionHandler()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _characterCombat.StartInputAction();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _characterCombat.EndInputAction();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public GameplayCharacterController characterController;
    Vector2 moveDir;

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        characterController.Move(moveDir);
    }
}

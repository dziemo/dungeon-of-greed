using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Rigidbody rb;
    Vector2 moveDir;

    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    private void Update()
    {
        rb.velocity = new Vector3(moveDir.x, 0, moveDir.y);
    }
}

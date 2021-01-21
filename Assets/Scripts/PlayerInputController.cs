using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public GameplayCharacterController characterController;
    public LayerMask groundMask;

    bool mouseController;
    Camera cam;
    Vector3 mousePos;

    PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        var deviceName = playerInput.devices[0].device.displayName;

        if (deviceName == "Mouse" || deviceName == "Keyboard")
        {
            mouseController = true;
            cam = Camera.main;
        }
    }

    private void OnMove(InputValue value)
    {
        var moveDir = value.Get<Vector2>();
        characterController.Move(moveDir);
    }

    private void OnLook(InputValue value)
    {
        if (mouseController)
        {
            mousePos = value.Get<Vector2>();
        }
        else
        {
            var lookDir = value.Get<Vector2>();
            characterController.LookGamepad(lookDir);
        }
    }

    private void OnAttack()
    {
        characterController.Attack();
    }

    private void OnAlternativeAttack()
    {

    }

    private void OnDash()
    {
        characterController.Dash();
    }

    private void OnInteract ()
    {
        characterController.Interact();
    }

    private void Update()
    {
        if (mouseController)
        {
            Vector3 tempMousePos = cam.ScreenToWorldPoint(mousePos - new Vector3(0, 0, cam.transform.position.z));
            Ray mouseRay = new Ray(cam.transform.position, tempMousePos - cam.transform.position);
            RaycastHit mouseRayHit;
            Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.red, 1f);

            if (Physics.Raycast(mouseRay, out mouseRayHit, 100f, groundMask))
            {
                characterController.LookMouse(mouseRayHit.point);
            }
        }
    }
}

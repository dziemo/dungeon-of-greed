using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCharacterController : MonoBehaviour
{
    public Transform bodyPos;
    public Transform headPos;
    public Transform armLeftPos;
    public Transform armRightPos;
    public Transform weaponPos;

    public float characterSpeed = 5f;
    public float itemCheckRadius = 2f;
    public LayerMask interactablesLayer;

    WeaponController currentWeapon;
    Animator anim;
    CharacterController characterController;
    Vector3 moveDir;
    Vector3 lookDir;
    Interactable interactable;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        lookDir = transform.forward;
        anim = GetComponent<Animator>();
    }

    public void SpawnPlayer(PlayerData player)
    {
        Instantiate(player.headPrefab, headPos.position, headPos.rotation, headPos);
        Instantiate(player.characterBody.bodyPrefab, bodyPos.position, bodyPos.rotation, bodyPos);
        Instantiate(player.characterBody.armLeftPrefab, armLeftPos.position, armLeftPos.rotation, armLeftPos);
        var rightArm = Instantiate(player.characterBody.armRightPrefab, armRightPos.position, armRightPos.rotation, armRightPos);
        weaponPos = rightArm.transform.GetChild(0);
    }

    public void LookGamepad(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            lookDir = new Vector3(dir.x, 0, dir.y);
        }
    }

    public void LookMouse(Vector3 point)
    {
        point.y = transform.position.y;
        lookDir = (point - transform.position).normalized;
    }

    public void Move (Vector3 dir)
    {
        moveDir = new Vector3(dir.x, 0, dir.y);
    }

    public void Attack()
    {
        if (currentWeapon)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void Interact ()
    {
        if (interactable)
        {
            interactable.Interact(gameObject);
        }
    }

    private void Update()
    {
        characterController.transform.forward = lookDir;

        if (moveDir != Vector3.zero)
        {
            characterController.Move(moveDir * characterSpeed * Time.deltaTime);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        InteractableCheck();
    }

    private void InteractableCheck()
    {
        var interactables = Physics.OverlapSphere(transform.position, itemCheckRadius, interactablesLayer, QueryTriggerInteraction.Collide);
        if (interactables.Length > 0)
        {
            interactable = interactables[0].GetComponent<Interactable>();
        }
        else
        {
            interactable = null;
        }
    }

    public void OnPickup (WeaponController controller, GameObject weaponObject)
    {
        currentWeapon = controller;
        weaponObject.transform.SetParent(weaponPos);
        weaponObject.transform.localPosition = Vector3.zero;
        weaponObject.transform.localRotation = Quaternion.identity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, itemCheckRadius);
    }
}

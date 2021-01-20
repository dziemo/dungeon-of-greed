﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCharacterController : MonoBehaviour
{
    public Transform bodyPos;
    public Transform headPos;
    public Transform armLeftPos;
    public Transform armRightPos;

    public float characterSpeed = 5f;
    public float itemCheckRadius = 2f;
    public LayerMask interactablesLayer;

    Rigidbody rb;
    Vector3 moveDir;
    Vector3 lookDir;
    Interactable interactableItem;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lookDir = transform.forward;
    }

    public void SpawnPlayer(PlayerData player)
    {
        Instantiate(player.headPrefab, headPos);
        Instantiate(player.characterBody.bodyPrefab, bodyPos);
        Instantiate(player.characterBody.armLeftPrefab, armLeftPos);
        Instantiate(player.characterBody.armRightPrefab, armRightPos);
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

    public void Interact ()
    {
        if (interactableItem)
        {
            interactableItem.Interact();
        }
    }

    private void Update()
    {
        rb.MoveRotation(Quaternion.LookRotation(lookDir, Vector3.up));
        rb.MovePosition(rb.position + (moveDir * characterSpeed * Time.deltaTime));
        InteractableCheck();
    }

    private void InteractableCheck()
    {
        var interactables = Physics.OverlapSphere(transform.position, itemCheckRadius, interactablesLayer);
        if (interactables.Length > 0)
        {
            interactableItem = interactables[0].GetComponent<Interactable>();
        }
        else
        {
            interactableItem = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, itemCheckRadius);
    }
}

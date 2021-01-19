using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCharacterController : MonoBehaviour
{
    public Transform bodyPos;
    public Transform headPos;
    public Transform armLeftPos;
    public Transform armRightPos;

    public float characterSpeed = 5f;

    Rigidbody rb;
    Vector3 moveDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SpawnPlayer(PlayerData player)
    {
        Instantiate(player.headPrefab, headPos);
        Instantiate(player.characterBody.bodyPrefab, bodyPos);
        Instantiate(player.characterBody.armLeftPrefab, armLeftPos);
        Instantiate(player.characterBody.armRightPrefab, armRightPos);
    }

    public void Move (Vector3 dir)
    {
        moveDir = new Vector3(dir.x, 0, dir.y);
    }

    private void Update()
    {
        rb.MovePosition(rb.position + (moveDir * characterSpeed * Time.deltaTime));
    }
}

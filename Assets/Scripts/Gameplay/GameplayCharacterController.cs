using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCharacterController : MonoBehaviour
{
    public Transform bodyPos;
    public Transform headPos;
    public Transform armLeftPos;
    public Transform armRightPos;

    public void SpawnPlayer(PlayerData player)
    {
        Instantiate(player.headPrefab, headPos);
        Instantiate(player.characterBody.bodyPrefab, bodyPos);
        Instantiate(player.characterBody.armLeftPrefab, armLeftPos);
        Instantiate(player.characterBody.armRightPrefab, armRightPos);
    }
}

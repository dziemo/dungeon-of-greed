using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ScriptableObjectArchitecture;

public class InputManagerController : MonoBehaviour
{
    public GameObjectGameEvent playerJoined;

    private void OnPlayerJoined (PlayerInput player)
    {
        player.transform.SetParent(transform);
        playerJoined.Raise(player.gameObject);
    }
}

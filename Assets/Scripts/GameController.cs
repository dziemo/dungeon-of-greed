using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameplayePlayerPrefab;

    private void Start()
    {
        foreach (var p in PlayerInput.all)
        {
            p.SwitchCurrentActionMap("Player");
            var character = Instantiate(gameplayePlayerPrefab, Vector3.one * Random.Range(-3, 3), Quaternion.identity, transform);
            character.GetComponent<GameplayCharacterController>().SpawnPlayer(p.GetComponent<PlayerData>());
            var pInput = p.GetComponent<PlayerInputController>();
            pInput.enabled = true;
            pInput.rb = character.GetComponent<Rigidbody>();
        }
    }

    [ContextMenu("Return To Lobby")]
    public void OnReturnToLobby ()
    {
        SceneManager.UnloadSceneAsync("GameScene");
        SceneManager.LoadSceneAsync("LobbyScene", LoadSceneMode.Additive);

        foreach (var p in PlayerInput.all)
        {
            p.SwitchCurrentActionMap("UI");
            var pInput = p.GetComponent<PlayerInputController>();
            pInput.enabled = false;
            pInput.rb = null;
        }
    }
}

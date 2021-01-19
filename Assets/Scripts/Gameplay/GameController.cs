using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameplayePlayerPrefab;
    public GameObjectCollection levels;

    GameObject currentLevel;
    LevelController currentLevelController;

    private void Start()
    {
        SpawnLevel();
        SpawnPlayers();
    }


    private void SpawnLevel ()
    {
        currentLevel = Instantiate(levels[0], transform);
        currentLevelController = currentLevel.GetComponent<LevelController>();
    }

    private void SpawnPlayers()
    {
        for (int i = 0; i < PlayerInput.all.Count; i++)
        {
            var p = PlayerInput.all[i];
            p.SwitchCurrentActionMap("Player");
            var character = Instantiate(gameplayePlayerPrefab, currentLevelController.spawns[i].position, currentLevelController.spawns[i].rotation, transform);
            character.GetComponent<GameplayCharacterController>().SpawnPlayer(p.GetComponent<PlayerData>());
            var pInput = p.GetComponent<PlayerInputController>();
            pInput.characterController = character.GetComponent<GameplayCharacterController>();
            pInput.enabled = true;
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
            pInput.characterController = null;
        }
    }
}

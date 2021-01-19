using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public Transform panelsParent, eventsParent, lobbyCharactersPanel;

    public GameObject eventSystem;
    public GameObject playerPanel;
    public GameObject lobbyCharacterPrefab;

    private void Start()
    {
        if (!SceneManager.GetSceneByName("UtilityScene").isLoaded)
        {
            SceneManager.LoadSceneAsync("UtilityScene", LoadSceneMode.Additive);
        }

        foreach (var p in PlayerInput.all)
        {
            OnPlayerJoined(p.gameObject);
        }
    }

    [ContextMenu("Start Game")]
    public void OnStartGame ()
    {
        PlayerInputManager.instance.DisableJoining();
        SceneManager.UnloadSceneAsync("LobbyScene");
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
    }

    public void OnPlayerJoined (GameObject player)
    {
        var eventSys = Instantiate(eventSystem, eventsParent);
        var panel = Instantiate(playerPanel, panelsParent);
        var selectionCtlr = panel.GetComponent<SelectionController>();
        var multiEventSystem = eventSys.GetComponent<MultiplayerEventSystem>();
        var lobbyCharacterCtrl = Instantiate(lobbyCharacterPrefab, selectionCtlr.playerCharacterPos.position, selectionCtlr.playerCharacterPos.rotation, selectionCtlr.playerCharacterPos).GetComponent<LobbyCharacterController>();
        var playerData = player.GetComponent<PlayerData>();
        
        player.GetComponent<PlayerInput>().uiInputModule = eventSys.GetComponent<InputSystemUIInputModule>();
       
        multiEventSystem.playerRoot = panel;
        multiEventSystem.firstSelectedGameObject = selectionCtlr.firstSelected;

        selectionCtlr.Setup(lobbyCharacterCtrl);

        lobbyCharacterCtrl.InitializeCharacter(playerData);
    }
}

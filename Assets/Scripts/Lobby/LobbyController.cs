using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public Transform panelsParent, eventsParent;

    public GameObject eventSystem;
    public GameObject playerPanel;
    public GameObject lobbyCharacterPrefab;

    Dictionary<GameObject, bool> playersReady = new Dictionary<GameObject, bool>();

    Coroutine startCoroutine;

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

        playersReady.Add(player, false);

        player.GetComponent<PlayerInput>().uiInputModule = eventSys.GetComponent<InputSystemUIInputModule>();
       
        multiEventSystem.playerRoot = panel;
        multiEventSystem.firstSelectedGameObject = selectionCtlr.firstSelected;

        selectionCtlr.Setup(player, lobbyCharacterCtrl);

        lobbyCharacterCtrl.InitializeCharacter(playerData);
        
        if (startCoroutine != null)
        {
            StopCoroutine(startCoroutine);
            startCoroutine = null;
        }
    }

    public void OnPlayerReady (GameObject player)
    {
        playersReady[player] = !playersReady[player];

        foreach (var p in playersReady)
        {
            if (!p.Value)
            {
                if (startCoroutine != null)
                {
                    StopCoroutine(startCoroutine);
                    startCoroutine = null;
                }
                return;
            }
        }

        startCoroutine = StartCoroutine(CountdownGameStart());
    }

    IEnumerator CountdownGameStart ()
    {
        yield return new WaitForSeconds(3);
        OnStartGame();
    }

}

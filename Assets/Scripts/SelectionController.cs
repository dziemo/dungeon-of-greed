using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionController : MonoBehaviour
{
    public GameObject firstSelected;
    public Transform playerCharacterPos;
    public Button nextHeadButton;
    public Button previousHeadButton;
    public Button nextBodyButton;
    public Button previousBodyButton;
    public Button readyButton;

    LobbyCharacterController characterController;

    public void Setup(LobbyCharacterController controller)
    {
        characterController = controller;
        nextHeadButton.onClick.AddListener(() => characterController.LoadNextHead());
        previousHeadButton.onClick.AddListener(() => characterController.LoadPreviousHead());

        nextBodyButton.onClick.AddListener(() => characterController.LoadNextBody());
        previousBodyButton.onClick.AddListener(() => characterController.LoadPreviousBody());

        readyButton.onClick.AddListener(
            () => {
                characterController.OnReady();
                nextHeadButton.enabled = false;
                previousHeadButton.enabled = false;
                nextBodyButton.enabled = false;
                previousBodyButton.enabled = false;
                }
            );
    } 

}

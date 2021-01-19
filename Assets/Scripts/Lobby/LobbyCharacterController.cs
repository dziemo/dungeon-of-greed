using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacterController : MonoBehaviour
{
    public GameObjectCollection headCollection;
    public CharacterBodyCollection bodyCollection;
    public PlayerData playerData;

    public Transform bodyPos;
    public Transform headPos;
    public Transform armLeftPos;
    public Transform armRightPos;

    GameObject currentBody;
    GameObject currentHead;
    GameObject currentArmLeft;
    GameObject currentArmRight;

    int headIndex = 0;
    int bodyIndex = 0;
    
    public void InitializeCharacter (PlayerData data)
    {
        playerData = data;
        
        if (playerData.characterBody != null)
        {
            headIndex = headCollection.List.IndexOf(playerData.headPrefab);
            bodyIndex = bodyCollection.List.IndexOf(playerData.characterBody);
        }

        SwapHead(headCollection[headIndex]);
        SwapBody(bodyCollection[bodyIndex]);
    }

    public void LoadNextHead ()
    {
        var result = GetNextItem(headCollection, headIndex);
        SwapHead(result.Item1);
        headIndex = result.Item2;
    }

    public void LoadPreviousHead()
    {
        var result = GetPreviousItem(headCollection, headIndex);
        SwapHead(result.Item1);
        headIndex = result.Item2;
    }

    public void LoadNextBody ()
    {
        var result = GetNextItem(bodyCollection, bodyIndex);
        SwapBody(result.Item1);
        bodyIndex = result.Item2;
    }

    public void LoadPreviousBody ()
    {
        var result = GetPreviousItem(bodyCollection, bodyIndex);
        SwapBody(result.Item1);
        bodyIndex = result.Item2;
    }

    private (T, int) GetNextItem<T> (Collection<T> collection, int index)
    {
        index = ++index % collection.Count;
        return ((T)collection.List[index], index);
    }

    private (T, int) GetPreviousItem<T> (Collection<T> collection, int index)
    {
        index--;

        if (index < 0)
        {
            index = collection.Count - 1;
        }

        return ((T)collection.List[index], index);
    }

    private void SwapHead (GameObject head)
    {
        if (currentHead)
        {
            Destroy(currentHead);
        }
        currentHead = Instantiate(head, headPos);
    }

    private void SwapBody (CharacterBody body)
    {
        if (currentBody)
        {
            Destroy(currentBody);
            Destroy(currentArmLeft);
            Destroy(currentArmRight);
        }

        currentBody = Instantiate(body.bodyPrefab, bodyPos);
        currentArmLeft = Instantiate(body.armLeftPrefab, armLeftPos);
        currentArmRight = Instantiate(body.armRightPrefab, armRightPos);
    }
    
    public void OnReady()
    {
        playerData.characterBody = bodyCollection[bodyIndex];
        playerData.headPrefab = headCollection[headIndex];
    }
}

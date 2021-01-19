using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "NewCharacterBody", menuName = "Character Body")]
public class CharacterBody : SOArchitectureBaseObject
{
    public GameObject bodyPrefab;
    public GameObject armLeftPrefab;
    public GameObject armRightPrefab;
}

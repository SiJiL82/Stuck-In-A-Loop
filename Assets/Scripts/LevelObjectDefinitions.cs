using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelObjectDefinition")]
public class LevelObjectDefinitions : ScriptableObject
{
    [SerializeField] LevelObjectDefinition[] levelObjects;


    public class LevelObjectDefinition
    {
        GameObject prefab;
        string colourName;
    }
}

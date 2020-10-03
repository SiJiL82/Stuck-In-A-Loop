using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    //Config
    //Variables
    //References
    [Header("Debug")]
    [SerializeField] int numRedObjects;
    [SerializeField] int numBlueObjects;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GetComponent<LevelManager>();

        StartCoroutine(GameLoop());
        Debug.Log("Level Completed");
    }

    IEnumerator GameLoop()
    {
        numRedObjects = levelManager.CountObjectsByShapeAndColour(colour:"red");
        numBlueObjects = levelManager.CountObjectsByShapeAndColour(colour:"blue");

        while(numRedObjects <= numBlueObjects)
        {
            numRedObjects = levelManager.CountObjectsByShapeAndColour(colour:"red");
            numBlueObjects = levelManager.CountObjectsByShapeAndColour(colour:"blue");
            Debug.Log($"RedObjects: {numRedObjects}");
            Debug.Log($"BlueObjects: {numBlueObjects}");

            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Ending game loop");
    }


}

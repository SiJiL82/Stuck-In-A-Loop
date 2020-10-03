using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Config
    [SerializeField] GameObject codeTextUI = null;
    [SerializeField] CodeScript codeScript = null;

    //Variables
    private int _numRedObjects = 0;
    public int numRedObjects {get{return _numRedObjects;} private set{}}

    private int _numBlueObjects = 0;
    public int numBlueObjects {get{return _numBlueObjects;} private set{}}

    private int _numGreenObjects = 0;
    public int numGreenObjects {get{return _numGreenObjects;} private set{}}

    private int _numYellowObjects = 0;
    public int numYellowObjects {get{return _numYellowObjects;} private set{}}

    private int _numSquares = 0;
    public int numSquares {get{return _numSquares;} private set{}}

    private int _numTriangles = 0;
    public int numTriangles{get{return _numTriangles;} private set{}}

    private int _numCircles = 0;
    public int numCircles{get{return _numCircles;} private set{}}


    //References
    
    void Start()
    {
        codeTextUI.GetComponent<UICodeText>().displayText = codeScript.codeText;
        CountLevelObjects();
    }

    
    void Update()
    {
        
    }

    public void CountLevelObjects()
    {
        ResetObjectCounts();

        Transform levelObjectParent = transform.Find("ObjectSpawnLocation");
        foreach (Transform child in levelObjectParent)
        {
            if (child.gameObject.tag == "LevelObject")
            {
                LevelObject childObject = child.gameObject.GetComponent<LevelObject>();
                switch(childObject.colourName)
                {
                    case "red" : _numRedObjects++;
                        break;
                    case "blue" : _numBlueObjects++;
                        break;
                    case "yellow" : _numYellowObjects++;
                        break;
                    case "green" : _numGreenObjects++;
                        break;
                    default : Debug.Log($"Level object with no colour name set: {child.gameObject.name}");
                        break;
                }
                switch(childObject.shapeName)
                {
                    case "square" : _numSquares++;
                        break;
                    case "triangle" : _numTriangles++;
                        break;
                    case "circle" : _numCircles++;
                        break;
                    default : Debug.Log($"Level object with no shape name set: {child.gameObject.name}");
                        break;
                }
            }
        }
    }

    private void ResetObjectCounts()
    {
        _numRedObjects = 0;
        _numBlueObjects = 0;
        _numGreenObjects = 0;
        _numYellowObjects = 0;
        _numSquares = 0;
        _numTriangles = 0;
        _numCircles = 0;
    }

    public int CountObjectsByShapeAndColour(string shape = null, string colour = null)
    {
        int countObjects = 0;
        Transform levelObjectParent = transform.Find("ObjectSpawnLocation");
        foreach (Transform child in levelObjectParent)
        {
            LevelObject childObject = child.gameObject.GetComponent<LevelObject>();
            if((childObject.colourName == colour || colour is null) && (childObject.shapeName == shape || shape is null))
            {
                countObjects++;
            }
        }

        return countObjects;
    }
}

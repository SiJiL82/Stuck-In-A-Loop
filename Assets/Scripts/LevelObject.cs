using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Color32 _materialColour;
    [SerializeField] private string _shapeName = null;
    private string _colourName = null;
    public string colourName{get{return _colourName;} private set{}}
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

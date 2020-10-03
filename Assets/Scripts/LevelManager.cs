using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Config
    [SerializeField] GameObject codeTextUI = null;
    [SerializeField] CodeScript codeScript = null;

    //Variables

    //References
    
    void Start()
    {
        codeTextUI.GetComponent<UICodeText>().displayText = codeScript.codeText;
    }

    
    void Update()
    {
        
    }


}

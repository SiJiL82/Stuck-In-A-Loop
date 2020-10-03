using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CodeScript")]
public class CodeScript : ScriptableObject
{
    [TextArea(10,14)][SerializeField] string _codeText;
    public string codeText{get{return _codeText;} private set{}}
}

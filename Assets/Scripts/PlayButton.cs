using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private bool _hasBeenClicked;
    public bool hasBeenClicked {get{return _hasBeenClicked;} private set{_hasBeenClicked = value;}}

}

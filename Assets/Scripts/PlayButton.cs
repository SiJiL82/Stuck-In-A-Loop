using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private bool _hasBeenClicked;
    public bool hasBeenClicked {get{return _hasBeenClicked;} private set{_hasBeenClicked = value;}}

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => FindObjectOfType<LevelLoader>().LoadSceneByName("Level 1", 2f));
        button.onClick.AddListener(() => SetClicked());
    }

    private void SetClicked()
    {
        _hasBeenClicked = true;
    }
}

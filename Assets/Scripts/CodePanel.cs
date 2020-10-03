using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePanel : MonoBehaviour
{   
    //Config
    

    //Variables
    float panelWidth;
    float panelHeight;

    //References


    // Start is called before the first frame update
    void Start()
    {
        RectTransform parentRectTransform = transform.parent.gameObject.GetComponent<RectTransform>();
        panelWidth = parentRectTransform.sizeDelta.x / 2;
        panelHeight = parentRectTransform.sizeDelta.y;
        SetDimensions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDimensions()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        //rectTransform.position = new Vector3(-55f, 0f, 0f);
        //rectTransform.sizeDelta = new Vector2(110f, 100f);
        rectTransform.anchoredPosition = new Vector3(-(panelWidth / 2), 0f, 0f);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, panelWidth);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, panelHeight);

    }
}

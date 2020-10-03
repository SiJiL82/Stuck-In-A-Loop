using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICodeText : MonoBehaviour
{
    //Config
    [SerializeField] float LoadTextAnimationSpeed = 0.3f;

    //Variables
    private string _displayText;
    public string displayText
    {
        get
        {
            return _displayText;
        }
        set
        {
            _displayText = value;
            LoadNewDisplayText();
        }
    }

    private GameObject parentPanel;

    //References
    private Text codeText;

    // Start is called before the first frame update
    void Start()
    {
        parentPanel = transform.parent.gameObject;
        codeText = GetComponent<Text>();
        SetDimensions();
    }

    private void LoadNewDisplayText()
    {

        //codeText.text = displayText;
        StartCoroutine(AnimateText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDimensions()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        RectTransform parentRectTransform = parentPanel.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = parentRectTransform.anchoredPosition;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parentRectTransform.sizeDelta.x);
    }

    IEnumerator AnimateText()
    {
        while(codeText.text.Length < _displayText.Length)
        {
            Debug.Log(codeText.text.Length);
            Debug.Log(_displayText[codeText.text.Length]);
            Debug.Log($"codeText: {codeText.text}");
            codeText.text = string.Concat(codeText.text, _displayText[codeText.text.Length]);
            yield return new WaitForSeconds(LoadTextAnimationSpeed);
        }
    }
}

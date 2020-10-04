using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICodeText : MonoBehaviour
{
    //Config
    [SerializeField] float LoadTextAnimationSpeed = 0.3f;
    [SerializeField] AudioClip sfxTextTyping = null;
    [SerializeField] float sfxTextTypingVolume = 0.5f;

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
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        parentPanel = transform.parent.gameObject;
        codeText = GetComponent<Text>();
        SetDimensions();
        audioSource = GetComponent<AudioSource>();
    }

    private void LoadNewDisplayText()
    {
        SetDimensions();
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
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parentRectTransform.sizeDelta.x - 60f);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentRectTransform.sizeDelta.y - 60f);
    }

    IEnumerator AnimateText()
    {
        while(codeText.text.Length < _displayText.Length)
        {
            codeText.text = string.Concat(codeText.text, _displayText[codeText.text.Length]);
            audioSource.PlayOneShot(sfxTextTyping, sfxTextTypingVolume);
            yield return new WaitForSeconds(LoadTextAnimationSpeed);
        }
    }
}

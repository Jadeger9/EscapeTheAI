using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _backgroundImage;

    private void OnEnable()
    {
        EventBus<OnSubtitleChange>.Subscribe(ChangeText);
    }

    private void OnDisable()
    {
        EventBus<OnSubtitleChange>.UnSubscribe(ChangeText);
    }

    private void ChangeText(OnSubtitleChange onSubtitleChange)
    {
        if (_text != null) _text.text = onSubtitleChange.value;
    }

    private void Update()
    {
        if (_text != null && _backgroundImage != null)
        {
            if (_text.text == "") _backgroundImage.gameObject.SetActive(false);
            else _backgroundImage.gameObject.SetActive(true);
        }        
    }
}

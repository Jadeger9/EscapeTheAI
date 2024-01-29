using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadingScreen1 : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _newSprite;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private int _loadingTime = 5;

    private void Start()
    {
        Invoke("StopLoading", _loadingTime);
    }

    private void StopLoading()
    {
        if (_image != null && _startButton != null && _newSprite != null)
        {
            _image.sprite = _newSprite;
            _startButton.SetActive(true);
        }
    }
}

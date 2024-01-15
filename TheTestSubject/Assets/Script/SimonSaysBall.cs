using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysBall : MonoBehaviour
{
    [SerializeField] private int _ballIndex;
    [SerializeField] private float _lightDuration = 0.75f;
    [SerializeField] private float _clickCooldown = 0.5f; // Cooldown for clicking the ball

    private Renderer _myRenderer;
    private Color _originalColor;
    private bool _isOnCooldown = false;

    private void OnEnable()
    {
        EventBus<OnBallColorChange>.Subscribe(LightOn);
    }

    private void Start()
    {
        TryGetComponent(out _myRenderer);
        if (_myRenderer != null) _originalColor = _myRenderer.material.color;
    }

    public void ClickBall()
    {
        if (!_isOnCooldown)
        {
            EventBus<OnBallClick>.Publish(new OnBallClick(_ballIndex));
            _isOnCooldown = true;
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(_clickCooldown);
        _isOnCooldown = false;
    }

    private void LightOn(OnBallColorChange onBallColorChange)
    {
        if (_ballIndex == onBallColorChange.value && _myRenderer != null)
        {
            _myRenderer.material.color = Color.green;
            Invoke("LightOff", _lightDuration);
            AudioManager.Instance.PlaySound("Pling");
        }
    }

    private void LightOff()
    {
        if (_myRenderer != null) _myRenderer.material.color = _originalColor;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlicker : MonoBehaviour
{
    public Image redOverlay;
    public float fadeDuration = 1f;
    public float flickerInterval = 0.1f;
    public float maxAlpha = 0.3f;

    private void OnEnable()
    {
        EventBus<OnFlickerStart>.Subscribe(StartFlicker);
        EventBus<OnFlickerEnd>.Subscribe(EndFlicker);
    }

    private void OnDisable()
    {
        EventBus<OnFlickerStart>.UnSubscribe(StartFlicker);
        EventBus<OnFlickerEnd>.UnSubscribe(EndFlicker);
    }

    private void StartFlicker(OnFlickerStart onFlickerStart)
    {
        StartCoroutine(FlickerScreen());
    }

    private void EndFlicker(OnFlickerEnd onFlickerEnd)
    {
        redOverlay.color = new Color(redOverlay.color.r, redOverlay.color.g, redOverlay.color.b, 0f);
        StopAllCoroutines();
    }

    IEnumerator FlickerScreen()
    {
        while (true)
        {
            yield return FadeRedOverlay(maxAlpha); // Fade in

            yield return new WaitForSecondsRealtime(flickerInterval);

            yield return FadeRedOverlay(0f); // Fade out

            yield return new WaitForSecondsRealtime(flickerInterval);

            // Add more flicker steps if needed

            yield return new WaitForSecondsRealtime(fadeDuration); // Wait for the total flicker duration
        }
    }

    IEnumerator FadeRedOverlay(float targetAlpha)
    {
        if (redOverlay == null)
        {
            yield break;
        }

        Color startColor = redOverlay.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, Mathf.Min(targetAlpha, maxAlpha));

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            redOverlay.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime; // Use Time.deltaTime here
            yield return null;
        }

        redOverlay.color = targetColor; // Ensure the target alpha is reached
    }
}

using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private Image[] image_array; // Array of sprites

    [SerializeField]
    private Sprite _targetSprite; // Serialized Image component

    [SerializeField]
    private GameObject _startButton; // Serialized GameObject component to be enabled at the end

    [SerializeField]
    private GameObject _blocks;

    private int currentIndex = 0; // Index to keep track of the current sprite

    private void Start()
    {
        if (_targetSprite == null)
        {
            Debug.LogError("Please assign the target Image component in the inspector.");
        }

        if (image_array == null || image_array.Length == 0)
        {
            Debug.LogError("Please assign sprites to the array in the inspector.");
        }

        if (_startButton == null)
        {
            Debug.LogError("Please assign the target GameObject component in the inspector.");
        }

        // Start switching sprites every second
        Invoke("SwitchSprite", 1f);
    }

    private void SwitchSprite()
    {
        // Check if there are sprites in the array
        if (image_array == null || image_array.Length == 0)
        {
            Debug.LogWarning("No sprites assigned to the array.");
            return;
        }

        // Update the image with the current sprite
        image_array[currentIndex].sprite = _targetSprite;

        // Move to the next sprite
        currentIndex++;

        // Check if reached the last sprite
        if (currentIndex >= image_array.Length)
        {
            // Enable the specified GameObject
            _startButton.SetActive(true);
            _blocks.SetActive(false);
            AudioManager.Instance.PlayShortSound("Loading");
    
        }
        else
        {
            float randomNum = Random.Range(2, 5);
            Invoke("SwitchSprite", randomNum);
            AudioManager.Instance.PlaySound("Pling");
        }
    }
}

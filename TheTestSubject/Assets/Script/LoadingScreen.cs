using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spriteArray; // Array of sprites

    [SerializeField]
    private Image targetImage; // Serialized Image component

    [SerializeField]
    private GameObject targetGameObject; // Serialized GameObject component to be enabled at the end

    private int currentIndex = 0; // Index to keep track of the current sprite

    private void Start()
    {
        if (targetImage == null)
        {
            Debug.LogError("Please assign the target Image component in the inspector.");
        }

        if (spriteArray == null || spriteArray.Length == 0)
        {
            Debug.LogError("Please assign sprites to the array in the inspector.");
        }

        if (targetGameObject == null)
        {
            Debug.LogError("Please assign the target GameObject component in the inspector.");
        }

        // Start switching sprites every second
        InvokeRepeating("SwitchSprite", 0f, 1f);
    }

    private void SwitchSprite()
    {
        // Check if there are sprites in the array
        if (spriteArray == null || spriteArray.Length == 0)
        {
            Debug.LogWarning("No sprites assigned to the array.");
            return;
        }

        // Update the image with the current sprite
        targetImage.sprite = spriteArray[currentIndex];

        // Move to the next sprite
        currentIndex++;

        // Check if reached the last sprite
        if (currentIndex >= spriteArray.Length)
        {
            // Enable the specified GameObject
            targetGameObject.SetActive(true);

            // Stop the repeating invocation
            CancelInvoke("SwitchSprite");
        }
    }
}

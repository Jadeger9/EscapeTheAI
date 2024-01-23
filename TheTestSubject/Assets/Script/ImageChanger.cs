using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class ImageChanger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Events to trigger when the event is completed")]
    UnityEvent onComplete;

    public List<Sprite> list_1; // List of sprites for the first UI Image
    public List<Sprite> list_2; // List of sprites for the second UI Image

    public Image image1; // Reference to the first UI Image component
    public Image image2; // Reference to the second UI Image component

    private int[] _answerArray = new int[] { 0, 2, 2, 1 };
    private int totalChanges = 3; // Total number of changes allowed
    private int changeCount = 0; // Counter for the number of changes
    private List<Sprite> usedSprites = new List<Sprite>(); // List to track used sprites

    public UnityEvent onPress => onComplete;


    void Start()
    {
        // Check if both Image components are assigned
        if (image1 == null || image2 == null)
        {
            Debug.LogError("Please assign both Image components in the inspector.");
            return;
        }

        SetRandomImage(0);
    }

    public void SetRandomImage(int chosenNumber)
    {
        if (changeCount < totalChanges)
        {
            if (chosenNumber != 0 || totalChanges >= _answerArray.Length) CheckAnswer(chosenNumber);
            changeCount++;

            ChangeImageSprite(image1, list_1);
            ChangeImageSprite(image2, list_2);
        }
        // Deactivate Image components if the limit is reached
        else
        {
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(false);
            AudioManager.Instance.PlaySound("TaskCompleted");
            onComplete.Invoke();
        }       
    }

    private void CheckAnswer(int answer)
    {
        if (_answerArray[changeCount] == answer) AudioManager.Instance.PlaySound("GoodChoice");
        else AudioManager.Instance.PlaySound("BadChoice");
    }

    private void ChangeImageSprite(Image img, List<Sprite> spriteList)
    {
        if (spriteList.Count == 0)
        {
            Debug.LogWarning("The sprite list is empty for " + img.name);
            return;
        }

        // Filter out used sprites from the current list
        var availableSprites = spriteList.Except(usedSprites).ToList();

        // Check if there are available sprites to set
        if (availableSprites.Count > 0)
        {
            // Get a random index within the range of the available sprites
            int randomIndex = Random.Range(0, availableSprites.Count);

            // Assign the sprite from the available sprites based on the random index to the Image component
            img.sprite = availableSprites[randomIndex];

            // Add the used sprite to the list of used sprites
            usedSprites.Add(availableSprites[randomIndex]);
        }
    }
}

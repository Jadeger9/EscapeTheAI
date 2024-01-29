using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class FakeOrNotTaskController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Events to trigger when the event is completed")]
    UnityEvent onComplete;

    public List<Sprite> spriteList_1; // List of sprites for the correct answers
    public List<Sprite> spriteList_2;   // List of sprites for the wrong answers

    public Image image1; // Reference to the first UI Image component
    public Image image2; // Reference to the second UI Image component

    private int[] _answerArray = new int[] { 0, 2, 2, 1 };
    private int totalChanges = 3; // Total number of changes allowed
    private int changeCount = 0; // Counter for the number of changes

    public UnityEvent onPress => onComplete;

    void Start()
    {
        // Check if both Image components are assigned
        if (image1 == null || image2 == null)
        {
            Debug.LogError("Please assign both Image components in the inspector.");
            return;
        }

        SetImageInOrder(0);
    }

    public void SetImageInOrder(int chosenNumber)
    {
        if (changeCount < totalChanges)
        {
            if (chosenNumber != 0 && totalChanges >= spriteList_1.Count) CheckAnswer(chosenNumber);
            changeCount++;

            SetNextImage(image1, spriteList_1);
            SetNextImage(image2, spriteList_2);
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
        if (answer == _answerArray[changeCount]) AudioManager.Instance.PlaySound("GoodChoice");
        else AudioManager.Instance.PlaySound("BadChoice");
    }

    private void SetNextImage(Image img, List<Sprite> spriteList)
    {
        if (spriteList.Count == 0)
        {
            Debug.LogWarning("The sprite list is empty for " + img.name);
            return;
        }

        // Check if there are available sprites to set
        if (changeCount <= totalChanges)
        {
            // Assign the sprite from the list based on the current change count to the Image component
            img.sprite = spriteList[changeCount - 1];
        }
        else Debug.LogWarning("changecount too high + " + changeCount);
    }
}

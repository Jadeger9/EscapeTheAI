using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public float totalTime = 60.0f; // Total countdown time in seconds
    private float currentTime; // Current time left

    public TextMeshProUGUI timerText; // Reference to TextMeshPro Text

    private bool isTimerRunning = false; // Flag to track if the timer is running

    private void Start()
    {
        currentTime = totalTime;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                currentTime = 0;
                isTimerRunning = false;
                // Timer reached zero, perform any necessary actions
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Public method to start the timer
    public void StartTimer()
    {
        isTimerRunning = true;
    }
}

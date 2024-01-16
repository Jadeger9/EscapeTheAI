using UnityEngine;
using TMPro;

public class MazeClock : MonoBehaviour
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
                CheckAndPlayAudio(); // Call the new function to check and play audio
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

    // New function to check and play audio at specific times
    void CheckAndPlayAudio()
    {
        int[] audioTimes = { 30, 15, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

        foreach (int time in audioTimes)
        {
            if (Mathf.FloorToInt(currentTime) == time)
            {
                string audioName = "Countdown_" + time;
                // Play audio at the specified time
                AudioManager.Instance.PlaySound(audioName);
                break;
            }
        }
    }
}
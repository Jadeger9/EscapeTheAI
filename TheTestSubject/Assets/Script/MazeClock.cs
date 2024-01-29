using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class MazeClock : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Events to trigger when the event is completed")]
    UnityEvent onComplete;

    public float totalTime = 90.0f; // Total countdown time in seconds
    private float currentTime; // Current time left

    private bool isTimerRunning = false; // Flag to track if the timer is running
    private bool _hasEnded = false;
    HashSet<int> playedTimes = new HashSet<int>();
    private bool _hasTriggered = false;

    public UnityEvent onPress => onComplete;

    private void OnEnable()
    {
        EventBus<OnMazeEnd>.Subscribe(EndTask);    
    }

    private void OnDisable()
    {
        EventBus<OnMazeEnd>.UnSubscribe(EndTask);
    }

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
                CheckAndPlayAudio(); // Call the function to check and play audio
            }
            else
            {
                currentTime = 0;
                isTimerRunning = false;
                EndTask(new OnMazeEnd());
            }
        }
    }

    // Public method to start the timer
    public void OnTriggerEnter()
    {
        if (isTimerRunning == false && _hasTriggered == false)
        {
            isTimerRunning = true;
            _hasTriggered = true;
        }        
    }

    // New function to check and play audio at specific times
    void CheckAndPlayAudio()
    {
        int[] audioTimes = { 60, 30, 15, 13, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        foreach (int time in audioTimes)
        {
            // Check if the time has already been played
            if (!playedTimes.Contains(time) && Mathf.FloorToInt(currentTime) == time)
            {
                string audioName = "Countdown_" + time;
                // Play audio at the specified time
                AudioManager.Instance.PlaySound(audioName);

                // Mark the time as played
                playedTimes.Add(time);

                break;
            }
        }
    }

    private void EndTask(OnMazeEnd onMazeEnd)
    {
        if (_hasEnded == false)
        {
            if (isTimerRunning == true) AudioManager.Instance.PlaySound("TaskCompleted");
            else AudioManager.Instance.PlaySound("TaskFailed");
            isTimerRunning = false;
            _hasEnded = true;
            onComplete.Invoke();
        }
    }

    private void DestroyMaze()
    {

    }
}

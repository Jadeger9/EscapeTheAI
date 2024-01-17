using UnityEngine;

public class SimonSaysGameController : MonoBehaviour
{
    [SerializeField] private int _ballAmount = 9;
    [SerializeField] private int _totalRounds = 5;

    public float roundCooldown = 1f; // Cooldown between rounds
    private int currentRound = 1; // Current round
    private int[] sequence; // Array to store the sequence of lit balls
    private int sequenceIndex; // Index to keep track of the current ball in the sequence
    private bool _gameOver = false;
    private int _correctAnswers;
    private bool _inputEnabled = false; // New flag to control input during sequence display

    private void OnEnable()
    {
        EventBus<OnBallClick>.Subscribe(OnBallClicked);
    }

    private void OnDisable()
    {
        EventBus<OnBallClick>.UnSubscribe(OnBallClicked);
        StopAllCoroutines();
    }

    private void Start()
    {
        // Initialize the sequence array with a length of the maximum round
        sequence = new int[_totalRounds];
        StartNewRound();
    }

    private void StartNewRound()
    {
        _inputEnabled = false; // Disable input during sequence display

        // Generate a new random sequence for the current round
        for (int i = 0; i < currentRound; i++)
        {
            sequence[i] = Random.Range(1, _ballAmount);
        }

        // Start showing the sequence
        StartCoroutine(ShowSequence());
    }

    private System.Collections.IEnumerator ShowSequence()
    {
        // Display each ball in the sequence with a cooldown between them
        foreach (int ballIndex in sequence)
        {
            Color newColor = Color.yellow;
            EventBus<OnBallColorChange>.Publish(new OnBallColorChange(ballIndex, newColor));
            yield return new WaitForSeconds(roundCooldown);
        }

        // Enable input after the sequence display is complete
        _inputEnabled = true;
    }

    // Called when a ball is clicked
    public void OnBallClicked(OnBallClick onBallClick)
    {
        if (_gameOver == false && _inputEnabled)
        {
            // Check if the clicked ball is the correct one in the sequence
            if (onBallClick.value == sequence[sequenceIndex])
            {
                AudioManager.Instance.PlaySound("GoodChoice");
                sequenceIndex++;
                _correctAnswers++;

                Color newColor = Color.green;
                EventBus<OnBallColorChange>.Publish(new OnBallColorChange(onBallClick.value, newColor));

                // Check if the player completed the entire sequence
                if (sequenceIndex == currentRound)
                {
                    // Start the next round after a cooldown
                    Invoke("StartNextRound", roundCooldown);
                }
            }
            else
            {
                AudioManager.Instance.PlaySound("BadChoice");
                Color newColor = Color.red;
                EventBus<OnBallColorChange>.Publish(new OnBallColorChange(onBallClick.value, newColor));

                // Wrong ball clicked, skip the rest of the current round and go to the next
                Invoke("StartNextRound", roundCooldown);
            }
        }
    }

    private void StartNextRound()
    {
        // Increment the round and reset the sequence index
        currentRound++;
        sequenceIndex = 0;

        if (currentRound <= _totalRounds)
        {
            _inputEnabled = false; // Disable input before starting the next round
            // Start the next round after a cooldown
            Invoke("StartNewRound", roundCooldown);
        }
        else
        {
            _gameOver = true;
            Invoke("StartEnding", 5);
            if (_correctAnswers > (_totalRounds / 2)) AudioManager.Instance.PlaySound("TaskCompleted");
            else AudioManager.Instance.PlaySound("TaskFailed");
        }
    }

    private void StartEnding()
    {
        EventBus<WallCollapseEvent>.Publish(new WallCollapseEvent());
    }
}

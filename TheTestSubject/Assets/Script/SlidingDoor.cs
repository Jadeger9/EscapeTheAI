using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public float maxSlideDistance = 5f; // Set the maximum slide distance in the Inspector
    public Vector3 slideDirection = Vector3.right; // Set the slide direction in the Inspector

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpen = false;
    private float slidingSpeed = 5f; // Adjust the speed of door movement

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition;
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        if (isOpen)
            targetPosition = CalculateTargetPosition(maxSlideDistance);
        else
            targetPosition = initialPosition;
    }

    private Vector3 CalculateTargetPosition(float distance)
    {
        float clampedDistance = Mathf.Clamp(distance, 0f, maxSlideDistance);
        return initialPosition + slideDirection * clampedDistance;
    }

    private void Update()
    {
        float step = slidingSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}

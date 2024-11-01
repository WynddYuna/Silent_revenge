using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LockpickPuzzle : MonoBehaviour
{
    public Slider timingSlider; // Reference to the slider
    public TMP_Text feedbackText; // TextMeshPro text for feedback
    public GameObject tryAgainText; // GameObject for "Try Again!" text
    public GameObject successText; // Text for success message
    public RectTransform targetZone; // Reference to the target zone for unlocking
    public GameObject lockpickPanel; // Reference to the panel that contains the lockpicking UI
    public Button startButton; // Reference to the start button
    public Button unlockButton; // Reference to the unlock button

    private float sliderSpeed = 0.5f; // Speed of the slider movement
    private bool isMovingRight = true; // Direction of slider movement
    private bool isUnlocked = false; // State of the lockpick puzzle
    private bool isPuzzleActive = false; // Track if the puzzle is active

    private void Start()
    {
        InitializePuzzle();
    }

    private void Update()
    {
        if (isPuzzleActive && !isUnlocked) // Only move the slider if the puzzle is active and not unlocked
        {
            MoveSlider();
        }
    }

    private void InitializePuzzle()
    {
        feedbackText.text = "Pick the Lock"; // Initial message
        tryAgainText.SetActive(false); // Hide "Try Again!" initially
        successText.SetActive(false); // Hide success text initially
        timingSlider.value = 0; // Start slider at 0
        unlockButton.gameObject.SetActive(false); // Hide unlock button initially

        // Add listeners for buttons
        startButton.onClick.AddListener(StartPuzzle);
        unlockButton.onClick.AddListener(TryUnlock);
    }

    private void MoveSlider()
    {
        // Move the slider back and forth
        timingSlider.value += (isMovingRight ? sliderSpeed : -sliderSpeed) * Time.deltaTime;

        // Change direction at ends
        if (timingSlider.value >= 1) isMovingRight = false;
        if (timingSlider.value <= 0) isMovingRight = true;
    }

    public void StartPuzzle()
    {
        Debug.Log("Start Puzzle Clicked!"); // Confirm start
        lockpickPanel.SetActive(true); // Show the lockpicking UI
        feedbackText.text = "Pick the Lock"; // Reset feedback text
        timingSlider.value = 0; // Reset slider
        isUnlocked = false; // Reset unlock state
        isPuzzleActive = true; // Activate the puzzle
        unlockButton.gameObject.SetActive(true); // Show unlock button when starting the puzzle
        startButton.gameObject.SetActive(false); // Hide the start button

        Debug.Log("Puzzle should now be active, and unlock button visible."); // Debug log
    }

    public void TryUnlock()
    {
        Debug.Log("Unlock Button Clicked!"); // Check if the button click registers

        if (isUnlocked || !isPuzzleActive)
        {
            Debug.Log(isUnlocked ? "Already unlocked!" : "Puzzle is not active!");
            return; // Prevent further action if already unlocked or puzzle is not active
        }

        float sliderPos = timingSlider.value; // Slider's current value (0 to 1 range)
        float targetMin = targetZone.anchorMin.x; // Start of target zone
        float targetMax = targetZone.anchorMax.x; // End of target zone

        Debug.Log($"Slider Position: {sliderPos}, Target Min: {targetMin}, Target Max: {targetMax}"); // Debug log

        // Check if within target zone with a margin of error for better feedback
        float marginOfError = 0.05f; // Adjust this value for more or less forgiveness
        if (sliderPos >= targetMin - marginOfError && sliderPos <= targetMax + marginOfError)
        {
            // Player succeeded
            feedbackText.gameObject.SetActive(false); // Hide feedback text when success
            StartCoroutine(ShowSuccessMessage()); // Call method to show success feedback
            isUnlocked = true;
            isPuzzleActive = false; // Stop slider movement

            // Do not close the puzzle immediately; wait for the coroutine to finish
        }
        else
        {
                       // Player failed
            Debug.Log("Failed!"); // Log failure
            feedbackText.text = ""; // Clear feedback
            StartCoroutine(ShowTryAgainText()); // Show "Try Again!" and reset
            timingSlider.value = 0; // Reset slider to start position
        }
    }

    private IEnumerator ShowTryAgainText()
    {
        // Disable the unlock button while showing the "Try Again!" message
        unlockButton.interactable = false;

        if (tryAgainText != null)
        {
            tryAgainText.SetActive(true); // Show "Try Again!"
            feedbackText.gameObject.SetActive(false); // Hide feedback text during "Try Again!" message
            yield return new WaitForSeconds(1.5f); // Wait before hiding
            tryAgainText.SetActive(false); // Hide "Try Again!" after delay
        }

        feedbackText.text = "Pick the Lock"; // Reset feedback text
        feedbackText.gameObject.SetActive(true); // Re-show feedback text

        // Re-enable the unlock button after the "Try Again!" message is gone
        unlockButton.interactable = true;
    }

    private IEnumerator ShowSuccessMessage()
    {
        if (successText != null)
        {
            successText.SetActive(true); // Show the success text
            yield return new WaitForSeconds(1.5f); // Wait for a duration to display the message
            successText.SetActive(false); // Hide the success text after the delay
        }

        feedbackText.text = "Pick the Lock"; // Reset feedback text after success
        feedbackText.gameObject.SetActive(true); // Re-show feedback text

        // Close the puzzle after showing the success message
        ClosePuzzle();
    }

    private void ClosePuzzle()
    {
        lockpickPanel.SetActive(false); // Deactivate the lockpicking panel
        unlockButton.gameObject.SetActive(false); // Hide unlock button after closing the puzzle
        startButton.gameObject.SetActive(true); // Re-enable start button
        isPuzzleActive = false; // Reset puzzle state
    }
}
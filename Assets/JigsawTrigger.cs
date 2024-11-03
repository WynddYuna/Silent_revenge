using UnityEngine;

public class JigsawTrigger : MonoBehaviour
{
    public GameObject JigsawPuzzlePanel; // Reference to the jigsaw puzzle UI
    private bool puzzleCompleted = false; // Track if the puzzle has been completed

    private void OnTriggerEnter2D(Collider2D other)
{
    // Check if the player enters the trigger and the puzzle is not completed
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player entered the trigger. Puzzle completed: " + puzzleCompleted);
        if (!puzzleCompleted)
        {
            JigsawPuzzlePanel.SetActive(true); // Activate the jigsaw puzzle
        }
    }
}

    // Call this method to mark the puzzle as completed
    public void CompletePuzzle()
    {
        puzzleCompleted = true; // Set puzzleCompleted to true
        JigsawPuzzlePanel.SetActive(false); // Optionally hide the puzzle panel if needed
    }
}
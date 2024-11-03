using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawController : MonoBehaviour
{
    [SerializeField]
    private Transform[] cat; // Array for puzzle pieces

    [SerializeField]
    private GameObject WinText; // UI text for win message

    [SerializeField]
    private GameObject puzzlePanel; // Panel containing the puzzle pieces

    public JigsawTrigger jigsawTrigger; // Reference to the JigsawTrigger component

    public static bool youWin; // Static variable to track win state
    private bool puzzleVisible = true; // Track if the puzzle is visible
    private bool puzzleCompleted = false; // Track if the puzzle has been completed

    void Start()
    {
        WinText.SetActive(false); // Hide win text initially
        youWin = false; // Initialize win state
    }

    void Update()
    {
        if (puzzleVisible && !puzzleCompleted && AreAllPiecesAligned()) // Check if puzzle is visible, not completed, and pieces are aligned
        {
            youWin = true; // Set win state
            Debug.Log("Puzzle Completed! Showing Win Text."); // Debug log
            StartCoroutine(ShowWinTextAndHidePuzzle()); // Start coroutine to show win text and hide puzzle
        }
    }

    private bool AreAllPiecesAligned()
    {
        float tolerance = 0.1f; // Allow for a small margin of error
        foreach (Transform piece in cat) // Check each puzzle piece
        {
            if (Mathf.Abs(piece.rotation.z) > tolerance) // If any piece is not aligned
            {
                return false; // Return false
            }
        }
        return true; // Return true if all pieces are aligned
    }

    private IEnumerator ShowWinTextAndHidePuzzle()
    {
        ShowWinText(); // Show win text
        Debug.Log("Win Text Displayed."); // Debug log
        yield return new WaitForSeconds(2f); // Wait for 2 seconds (or any duration you want)
        HidePuzzle(); // Hide the puzzle after the wait
    }

    private void HidePuzzle()
{
    puzzlePanel.SetActive(false); // Hide the panel containing the puzzle pieces
    puzzleVisible = false; // Set puzzle visibility to false
    puzzleCompleted = true; // Set puzzle completed to true
    jigsawTrigger.CompletePuzzle(); // Call the CompletePuzzle method on the JigsawTrigger instance
    Debug.Log("Puzzle is now hidden and marked as completed."); // Debug log
}

    private void ShowWinText()
    {
        WinText.SetActive(true); // Show win text
    }
}
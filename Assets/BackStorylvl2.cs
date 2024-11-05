using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackStory2 : MonoBehaviour
{
    public TextMeshProUGUI storyText; // Reference to the TextMeshPro component
    public Button nextButton; // Reference to the Next button
    public Button prevButton; // Reference to the Previous button

    private int currentIndex = 0; // Current index of the storyline
    private string[] storyLines = {
        "*Level 2: The Clue*",
        "Level 1: The Beginning",
        "While finding a clue about his sister, Elias finds a torn piece of a mysterious note leading him toward the haunted woods.",
        "Objectives:    Find the first clue about Lila's fate. Head towards the haunted woods.",
        "Game Mechanics:    Collect items and go to the next level"
    };

    void Start()
    {
        // Set up button listeners
        nextButton.onClick.AddListener(NextText);
        prevButton.onClick.AddListener(PrevText);
        
        // Display the first line of the story
        UpdateText();
    }

    void NextText()
    {
        if (currentIndex < storyLines.Length - 1)
        {
            currentIndex++;
            UpdateText();
        }
    }

    void PrevText()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateText();
        }
    }

    void UpdateText()
    {
        // Update the TextMeshPro text with the current story line
        storyText.text = storyLines[currentIndex];
    }
}
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
        "In the aftermath of Lila's death, Elias finds a torn piece of her favorite doll and a mysterious note leading him toward the haunted woods, filled with dark promises of vengeance.",
        "Objectives:    Find the first clue about Lila's fate. Head towards the haunted woods.",
        "Game Mechanics:    Collect items and read the note."
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
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackStorylvl4 : MonoBehaviour
{
    public TextMeshProUGUI storyText; // Reference to the TextMeshPro component
    public Button nextButton; // Reference to the Next button
    public Button prevButton; // Reference to the Previous button

    private int currentIndex = 0; // Current index of the storyline
    private string[] storyLines = {
        "*Level 4: The Abandoned House*",
        "An old, abandoned house looms ahead. Inside, Elias senses the lingering presence of the Shadow Witch, the source of his torment.",
        "Objectives:    Solve puzzles to progress through the house. Uncover clues about the Shadow Witch.",
        "Game Mechanics:    Puzzle-solving: Interact with objects to solve puzzles."
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
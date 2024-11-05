using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackStorylvl5 : MonoBehaviour
{
    public TextMeshProUGUI storyText; // Reference to the TextMeshPro component
    public Button nextButton; // Reference to the Next button
    public Button prevButton; // Reference to the Previous button

    private int currentIndex = 0; // Current index of the storyline
    private string[] storyLines = {
        "*Level 5: The Guardian*",
        "A fierce guardian (THE WITCH) blocks Elias's way. To proceed, he must defeat this formidable foe.",
        "Objectives:   Defeat the mini-boss. Find a significant clue about the Shadow Witch and Elias sister.",
        "Game Mechanics:    Combat: Use attack and dodge mechanics to fight the guardian."
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
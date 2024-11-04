using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackStory : MonoBehaviour
{
    public TextMeshProUGUI storyText; // Reference to the TextMeshPro component
    public Button nextButton; // Reference to the Next button
    public Button prevButton; // Reference to the Previous button

    private int currentIndex = 0; // Current index of the storyline
    private string[] storyLines = {
        "*Chapter 1: The Awakening*",
        "Level 1: The Beginning",
        "Elias's journey begins on a fateful night when he hears a strange noise that awakens his sister, Lila. As the moon casts eerie shadows, Elias warns Lila to stay inside.",
        "Objectives:    Learn basic controls and Experience Lila's awakening and her tragic fate.",
        "Game Mechanics:    Movement: Use arrow keys or joystick to move Elias. Interaction: Use the 'E' key or an on-screen button to interact with objects."
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
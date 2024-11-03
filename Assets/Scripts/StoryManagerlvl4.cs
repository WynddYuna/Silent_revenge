using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManagerlvl4 : MonoBehaviour
{
    public TextMeshProUGUI storyText; // Reference to the TextMeshPro component
    public Button nextButton; // Reference to the Next button
    public Button prevButton; // Reference to the Previous button

    private int currentIndex = 0; // Current index of the storyline
    private string[] storyLines = {
        "Your in Level 4 right now",
        "In this level you dont need to worry about dying???",
        "You can see a House on your way there. An abandon House!!!",
        "And on your way to the house youll face some puzzles that will test your mind that ever puzzle you solve, a DOOR will open!!!",
        "Your Goal is to solve all the puzzles and find some traces of your sister!??"
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
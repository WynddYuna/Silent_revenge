using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManagerlvl2 : MonoBehaviour
{
    public TextMeshProUGUI storyText; // Reference to the TextMeshPro component
    public Button nextButton; // Reference to the Next button
    public Button prevButton; // Reference to the Previous button

    private int currentIndex = 0; // Current index of the storyline
    private string[] storyLines = {
        "Your in Level 2 right now",
        "You need to find clues that point where your sister is!!!",
        "The clues is scattered around the village",
        "After you collected the papers, then you are ready to proceed to the next level!"
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
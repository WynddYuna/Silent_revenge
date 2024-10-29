using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelInstructions : MonoBehaviour
{
    public GameObject[] instructionTexts; // Array to hold your instruction texts
    public float displayDuration = 2f; // Duration to display each instruction
    private int currentInstructionIndex = 0;

    void Start()
    {
        // Start the coroutine to display instructions
        StartCoroutine(DisplayInstructions());
    }

    private IEnumerator DisplayInstructions()
    {
        // Loop through each instruction
        for (currentInstructionIndex = 0; currentInstructionIndex < instructionTexts.Length; currentInstructionIndex++)
        {
            // Show the current instruction
            instructionTexts[currentInstructionIndex].SetActive(true);
            yield return new WaitForSeconds(displayDuration);
            // Hide the current instruction
            instructionTexts[currentInstructionIndex].SetActive(false);
        }
    }
}
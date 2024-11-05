using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include this namespace to work with UI components

public class MenuController : MonoBehaviour
{

    [SerializeField] public GameObject gui;
    [SerializeField] public GameObject Bar;
    public GameObject menuCanvas; // Reference to the menu canvas

    [SerializeField] public GameObject InstructionsText;
    public Button menuButton; // Reference to the UI button
    public GameObject[] otherCanvases; // Array of other canvases to close


    private GameObject lastActiveCanvas; // Keep track of the last active canvas

    void Start()
    {
        menuCanvas.SetActive(false); // Ensure the menu is initially inactive
        menuButton.onClick.AddListener(OpenMenu); // Add listener to the button
    }

    void Update()
    {
        // Check if the Tab key is pressed to toggle the menu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();
        }
    }

    void OpenMenu()
    {
        ToggleMenu(); // Call the toggle method when the button is clicked
    }

    void ToggleMenu()
    {
        bool isMenuActive = menuCanvas.activeSelf; // Check if the menu is currently active

        if (!isMenuActive)
        {
            // Store the currently active canvas before opening the menu
            lastActiveCanvas = GetActiveCanvas();
            // Set the menu canvas active state
            menuCanvas.SetActive(true);
            // Close other canvases
            CloseOtherCanvases();
            gui.SetActive(false);
            Bar.SetActive(false);
            InstructionsText.SetActive(false);
            
        }
        else
        {
            // Close the menu
            menuCanvas.SetActive(false);
            gui.SetActive(true);
            Bar.SetActive(true);
            InstructionsText.SetActive(true);
            // Reopen the last active canvas if it exists
            if (lastActiveCanvas != null)
            {
                lastActiveCanvas.SetActive(true);
            }
        }
    }

    void CloseOtherCanvases()
    {
        foreach (GameObject canvas in otherCanvases)
        {
            canvas.SetActive(false); // Close each other canvas
        }
    }

    GameObject GetActiveCanvas()
    {
        foreach (GameObject canvas in otherCanvases)
        {
            if (canvas.activeSelf)
            {
                return canvas; // Return the currently active canvas
            }
        }
        return null; // No active canvas found
    }
}
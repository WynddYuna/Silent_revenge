using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include this namespace to work with UI components

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas; // Reference to the menu canvas
    public Button menuButton; // Reference to the UI button

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
        menuCanvas.SetActive(!menuCanvas.activeSelf); // Toggle the active state of the menu
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public static TabController instance;

    public Image[] tabImages;
    public GameObject[] pages;
    public Image[] bloodImages; // Changed to Image type to match your tab setup

    private bool isGamePaused = false;

    public bool IsGamePaused
    {
        get { return isGamePaused; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int tabNO)
    {
        Debug.Log("Activating Tab: " + tabNO);
        
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] == null) { Debug.LogError("Page " + i + " is null"); }
            if (tabImages[i] == null) { Debug.LogError("Tab Image " + i + " is null"); }
            if (bloodImages[i] == null) { Debug.LogError("Blood Image " + i + " is null"); }

            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
            bloodImages[i].enabled = false; // Disable all blood images initially
        }
        pages[tabNO].SetActive(true);
        tabImages[tabNO].color = Color.white;
        bloodImages[tabNO].enabled = true; // Enable blood image for the active tab
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}

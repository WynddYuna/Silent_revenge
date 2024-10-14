using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;

    // Start is called before the first frame update
    void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int tabNO)
    {
        if (tabImages != null && pages != null)
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if (pages[i] != null)
                {
                    pages[i].SetActive(false);
                }
                if (tabImages[i] != null)
                {
                    if (i == tabNO)
                    {
                        tabImages[i].sprite = bloodImage; // Set the blood image for the active tab
                        tabImages[i].color = Color.white; // You can also change the color if needed
                    }
                    else
                    {
                        tabImages[i].sprite = tabImages[i].sprite; // Reset the image of the inactive tabs to their default state
                        tabImages[i].color = Color.red; // Reset the color of the inactive tabs
                    }
                }
            }
            if (pages[tabNO] != null)
            {
                pages[tabNO].SetActive(true);
            }
        }
        else
        {
            Debug.LogError("tabImages or pages arrays are not initialized");
        }
    }

    public Sprite bloodImage; // Assign this in the Inspector window
}
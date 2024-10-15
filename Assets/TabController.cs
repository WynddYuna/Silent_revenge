using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;
    public Image[] bloodEffectImages;

    // Start is called before the first frame update
    void Start()
    {
        ActivateTab(0);
    }

    public void ActivateTab(int tabNO)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
            bloodEffectImages[i].enabled = false; // hide blood effect image
        }
        pages[tabNO].SetActive(true);
        tabImages[tabNO].color = Color.white;
        bloodEffectImages[tabNO].enabled = true; // show blood effect image
    }
}
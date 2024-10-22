using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu; // Reference to the inventory menu GameObject
    private bool menuActivated; 
    
    
    public ItemSlot[] itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the inventory menu is initially inactive
        InventoryMenu.SetActive(false);
        menuActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the "E" key press to toggle the inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            ToggleInventory();
        }
    }

    // Method to toggle the inventory menu
    private void ToggleInventory()
    {
        Debug.Log("ToggleInventory called");
        if (menuActivated)
        {
            // If the menu is active, deactivate it
            Time.timeScale = 1; // Resume game time
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else
        {
            // If the menu is not active, activate it
            Time.timeScale = 0; // Pause game time
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    // Method to add an item to the inventory
    public int AddItem(string itemName, int quantity, Sprite itemSprite,string itemDescription)
    {
        for(int i= 0; i< itemSlot.Length; i++)
        {

            if(itemSlot[i].isFull==false && itemSlot[i].itemName ==itemName|| itemSlot[i].quantity == 0) 
            {
               int leftOverItems= itemSlot[i].AddItem(itemName,quantity,itemSprite,itemDescription);
               if(leftOverItems >0)
                   leftOverItems=AddItem(itemName,leftOverItems,itemSprite,itemDescription);
                return leftOverItems;
            }


        }
        return quantity;
    }

      public void DeselectAllSlots(){

        for(int i=0; i< itemSlot.Length ; i++){

            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected=false;


        }
         
    }
}
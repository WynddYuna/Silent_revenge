using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Security.Cryptography;
using Unity.Collections.LowLevel.Unsafe;

public class ItemSlot : MonoBehaviour,IPointerClickHandler 


{

//ITEM DATA

public string itemName;
public int quantity;
public Sprite itemSprite;
public bool isFull;
public string itemDescription;


// ITEM SLOT

[SerializeField]
private TMP_Text quantityText;

[SerializeField]
private Image itemImage;



//ITEM DESCRIPTION
public  Image itemDescriptionImage;
public TMP_Text ItemDescriptionNameText;
public TMP_Text ItemDescriptionText;


public GameObject selectedShader;
public bool thisItemSelected;

private InventoryManager inventoryManager;  

private void Start(){
    inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
}


public void AddItem(string itemName, int quantity, Sprite itemSprite,string itemDescription){


    this.itemName = itemName;
    this.quantity = quantity;
    this.itemSprite = itemSprite;
    this.itemDescription= itemDescription;
    isFull= true;

    quantityText.text= quantity.ToString();

    quantityText.enabled=true;
    itemImage.sprite=itemSprite;
    itemImage.enabled=true;
}

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left){
             OnLeftClick();

        }
        
        if(eventData.button == PointerEventData.InputButton.Right){
            OnRightClick();
        }
    }

    public void OnLeftClick(){


        inventoryManager.DeselectAllSlots();


        selectedShader.SetActive(true);
        thisItemSelected=true;
        ItemDescriptionNameText.text =itemName;
        ItemDescriptionText.text =itemDescription;
        itemDescriptionImage.sprite=itemSprite;



    }

     public void OnRightClick(){



    }

    
}

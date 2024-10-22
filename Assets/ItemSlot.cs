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

[SerializeField]
private int maxNumberOfItems;


// ITEM SLOT

[SerializeField]
private TMP_Text quantityText;

[SerializeField]
public Image itemImage;



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


public int AddItem(string itemName, int quantity, Sprite itemSprite,string itemDescription){

    if(isFull)
       return quantity;

    this.itemSprite = itemSprite;
    itemImage.sprite=itemSprite;
    itemImage.enabled=true;

    this.itemName = itemName;
  
    this.itemDescription= itemDescription;

    this.quantity += quantity;
    if( this.quantity >= maxNumberOfItems){


     quantityText.text= quantity.ToString();

    quantityText.enabled=true;
     isFull= true;

   int  extraItems= this.quantity-maxNumberOfItems;
   this.quantity=maxNumberOfItems;
   return extraItems;
   }
   quantityText.text=maxNumberOfItems.ToString();
   quantityText.enabled= true;
   return 0;
 
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

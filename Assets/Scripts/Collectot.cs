using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectot : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player entered trigger with: " + collision.name);
        Iitem item = collision.GetComponent<Iitem>();
        
        if (item != null)
        {
            Debug.Log("Item found, collecting it...");
            item.Collect();
        }
        else
        {
            Debug.Log("Object does not have Iitem interface: " + collision.name);
        }
    }
}


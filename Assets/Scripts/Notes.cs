using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour , IItem
{
    public void Collect()
    {
        Destroy(gameObject);
    }

   
}
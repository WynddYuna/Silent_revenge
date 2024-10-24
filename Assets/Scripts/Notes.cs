using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes   : MonoBehaviour , IItem
{

    public static event Action<int> OnNotesCollect;
    public int worth = 5;
    public void Collect()
    {
        OnNotesCollect.Invoke(worth);
        
        Destroy(gameObject);
    }

   
}
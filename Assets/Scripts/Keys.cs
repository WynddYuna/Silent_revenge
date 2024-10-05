using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour , Iitem
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}

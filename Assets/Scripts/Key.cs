using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, Iitem
{
    public void Collect()
{
    Destroy(gameObject);
}
}
 
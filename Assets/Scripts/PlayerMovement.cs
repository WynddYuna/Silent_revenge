using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // Speed of the player movement

    void Update()
    {
        // Get input from horizontal axis (left/right or A/D keys)
        float move = Input.GetAxis("Horizontal");

        // Move the player horizontally
        transform.position += new Vector3(move, 0, 0) * speed * Time.deltaTime;
    }
}

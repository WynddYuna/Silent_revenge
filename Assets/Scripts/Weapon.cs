using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    public float offset;
    public GameObject projectile; // Ensure this is assigned in the Inspector
    public Transform shotPoint; // Ensure this is assigned in the Inspector

    private float timeBtwShots;
    public float startTimeBtwShots = 0.5f; // Set a reasonable default value

    private void Update()
    {
        // Handles the weapon rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; // Set this to the distance from the camera
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        
        Vector3 difference = worldMousePos - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0)) // Left mouse button
            {
                Debug.Log("Shooting projectile!");
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots; // Reset the shot timer
            }
        }
        else 
        {
            timeBtwShots -= Time.deltaTime; // Decrease the timer
        }
    }
}
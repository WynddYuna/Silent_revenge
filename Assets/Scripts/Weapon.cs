using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    public float offset; // Offset for weapon rotation
    public GameObject projectile; // Projectile prefab
    public Transform shotPoint; // Point from where the projectile is shot

    private float timeBtwShots; // Timer for shooting
    public float startTimeBtwShots = 0.5f; // Time between shots

    private void Update()
    {
        // Handle shooting
        if (timeBtwShots <= 0)
        {
            if (Input.touchCount > 0) // Check for touch input
            {
                Touch touch = Input.GetTouch(0);
                
                // Only process the touch if it began or moved
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    Vector3 touchPos = touch.position;
                    touchPos.z = Camera.main.nearClipPlane; // Set this to the distance from the camera
                    Vector3 worldTouchPos = Camera.main.ScreenToWorldPoint(touchPos);

                    // Calculate the direction to the touch position
                    Vector3 difference = worldTouchPos - transform.position;
                    float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

                    // Instantiate the projectile
                    Debug.Log("Shooting projectile!");
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots; // Reset the shot timer
                }
            }
        }
        else 
        {
            timeBtwShots -= Time.deltaTime; // Decrease the timer
        }
    }
}
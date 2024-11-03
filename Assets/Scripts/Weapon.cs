using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour 
{
    public GameObject projectile; // Assign your projectile prefab in the inspector
    public Transform shotPoint; // Assign your shot point in the inspector
    public float shootingCooldown = 0.5f; // Time between shots

    private float timeSinceLastShot;

    private void Update()
    {
        // Handle shooting
        if (timeSinceLastShot <= 0)
        {
            if (Input.touchCount > 0) // Check for touch input
            {
                Touch touch = Input.GetTouch(0);
                
                // Only process the touch if it began or moved
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    // Check if the touch is over a UI element
                    if (!IsTouchOverUI(touch))
                    {
                        Vector3 touchPos = touch.position;
                        touchPos.z = Camera.main.nearClipPlane; // Set to the distance from the camera
                        Vector3 worldTouchPos = Camera.main.ScreenToWorldPoint(touchPos);

                        // Calculate the direction to the touch position
                        Vector3 direction = (worldTouchPos - shotPoint.position).normalized;

                        // Rotate the weapon to face the touch position
                        RotateWeapon(direction);

                        // Instantiate the projectile
                        ShootProjectile(direction);
                        timeSinceLastShot = shootingCooldown; // Reset the shot timer
                    }
                }
            }
        }
        else 
        {
            timeSinceLastShot -= Time.deltaTime; // Decrease the timer
        }
    }

    private void ShootProjectile(Vector3 direction)
    {
        GameObject newProjectile = Instantiate(projectile, shotPoint.position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().Initialize(direction); // Pass the direction to the projectile
    }

    private void RotateWeapon(Vector3 direction)
    {
        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Apply the rotation to the weapon
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private bool IsTouchOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = touch.position;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("UI")) // Ensure your UI elements have the "UI" tag
            {
                return true;
            }
        }

        return false;
    }
}
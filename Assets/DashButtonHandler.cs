using UnityEngine;

public class DashButtonHandler : MonoBehaviour
{
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script

    // This method will be called when the button is clicked
    public void OnDashButtonClicked()
    {
        if (playerMovement != null)
        {
            playerMovement.DashButton(); // Call the DashButton method from PlayerMovement
        }
    }
}
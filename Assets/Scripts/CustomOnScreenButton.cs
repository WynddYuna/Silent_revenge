using UnityEngine;
using UnityEngine.EventSystems;

public class CustomOnScreenButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { Left, Right }
    public ButtonType buttonType;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement not found in the scene.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Call the appropriate movement method based on the button type
        if (buttonType == ButtonType.Left)
        {
            playerMovement.MoveLeft();
        }
        else if (buttonType == ButtonType.Right)
        {
            playerMovement.MoveRight();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Stop the player's movement when the button is released
        playerMovement.StopMoving();
    }
}
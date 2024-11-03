using UnityEngine;
using UnityEngine.EventSystems; // Ensure you have this for the pointer click

public class JigsawRotate : MonoBehaviour, IPointerClickHandler
{
    private void Start()
    {
        // Optionally, set an initial rotation
        int randomRotation = Random.Range(0, 4) * 90;
        transform.Rotate(0f, 0f, randomRotation);
    }

    // This method will be called when the object is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!JigsawController.youWin)
        {
            transform.Rotate(0f, 0f, 90f);
        }
    }
}

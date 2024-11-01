using UnityEngine;

public class TriggerZoneChecker : MonoBehaviour
{
    public LockpickPuzzle lockpickPuzzle; // Reference to the LockpickPuzzle script

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lockpickPuzzle.TryUnlock(); // Call TryUnlock on the lockpickPuzzle instance
        }
    }
}

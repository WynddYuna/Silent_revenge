using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Vector3 checkpointPosition;
    public Quaternion checkpointRotation;

    void Awake()
    {
        // Set the checkpoint position and rotation to the GameObject's transform values
        checkpointPosition = transform.position;
        checkpointRotation = transform.rotation;
    }
}

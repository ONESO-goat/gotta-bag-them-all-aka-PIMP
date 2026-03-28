using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // drag your Player here in Inspector
    public float smoothSpeed = 0.1f;
    public Vector3 offset = new Vector3(0, 0, -10); 
    // -10 on Z keeps the camera behind the 2D scene

    void LateUpdate()
    {
        // LateUpdate runs after Update — camera should always move last
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position, 
            desiredPosition, 
            smoothSpeed
        );
        // Lerp smoothly interpolates between current and desired position
        transform.position = smoothedPosition;
    }
}
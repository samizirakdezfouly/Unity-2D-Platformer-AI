using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 10.0f;

    public Vector3 cameraOffset;

    public bool isBound;
    public Vector3 minCameraPosition;
    public Vector3 maxCameraPosition;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        if(isBound)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPosition.x),
                Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPosition.y),
                Mathf.Clamp(transform.position.z, minCameraPosition.z, maxCameraPosition.z));
        }
    }
}

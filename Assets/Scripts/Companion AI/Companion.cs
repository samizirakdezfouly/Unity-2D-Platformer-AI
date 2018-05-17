using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour {

    private float raycastOriginOffset = 0.5f;

    public Transform xStart, xEnd, backXStart, backXEnd, yStart, yEnd;

    private int frontSensorDetectables;

    private int backSensorDetectables;

    private int overheadSensorDetecables;
	
	void Start ()
    {
        frontSensorDetectables = LayerMask.GetMask("Enemy","Cover", "Scavengable Object");
        backSensorDetectables = LayerMask.GetMask("Enemy");
        overheadSensorDetecables = LayerMask.GetMask("Enemy");
    }
	
    public void RaycastCheck()
    {
        RaycastHit2D frontRaycast = Physics2D.Linecast(xStart.position, xEnd.position,frontSensorDetectables);
        RaycastHit2D backRaycast = Physics2D.Linecast(backXStart.position, backXEnd.position,backSensorDetectables);
        RaycastHit2D overheadRaycast = Physics2D.Linecast(yStart.position, yEnd.position, overheadSensorDetecables);

        if (frontRaycast | backRaycast | overheadRaycast)
        {
            if (frontRaycast.collider != null)
            {
                Debug.DrawLine(xStart.position, xEnd.position, Color.green);
                Debug.Log("Companion Has Detected Ahead Of Itself: " + frontRaycast.collider.name);
            }

            if (backRaycast.collider != null)
            {
                Debug.DrawLine(backXStart.position, backXEnd.position, Color.green);
                Debug.Log("Companion Has Detected Behind Itself: " + backRaycast.collider.name);
            }

            if (overheadRaycast.collider != null)
            {
                Debug.DrawLine(yStart.position, yEnd.position, Color.green);
                Debug.Log("Companion Has Detected " + overheadRaycast.collider.name + " Above Itself");
            }
        }

    }

	void FixedUpdate ()
    {
        RaycastCheck();
	}
}

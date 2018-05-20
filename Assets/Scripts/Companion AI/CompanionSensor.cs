using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionSensor : MonoBehaviour {

    public Transform xStart, xEnd;

    private int frontSensorDetectables;

    public RaycastHit2D frontRaycast;

    void Start ()
    {
        frontSensorDetectables = LayerMask.GetMask("Enemy","Cover", "Scavengable Object");
        frontRaycast = Physics2D.Linecast(xStart.position, xEnd.position, frontSensorDetectables);
    }

    public void RaycastCheck()
    {
        frontRaycast = Physics2D.Linecast(xStart.position, xEnd.position, frontSensorDetectables);

        if (frontRaycast)
        {
            Debug.DrawLine(xStart.position, xEnd.position, Color.green);
        }
    }

    void FixedUpdate ()
    {
        RaycastCheck();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour {

    public GameWorld gameWorld;

    public PlayerCompanion2D companion2D;

    private float raycastOriginOffset = 0.5f;

    public Transform xStart, xEnd;

    private int frontSensorDetectables;

    public GameObject pickedUpHealth;

    private bool isCarryingObj = false;
	
	void Start ()
    {
        frontSensorDetectables = LayerMask.GetMask("Enemy","Cover", "Scavengable Object");
    }
	
    public void RaycastCheck()
    {
        RaycastHit2D frontRaycast = Physics2D.Linecast(xStart.position, xEnd.position,frontSensorDetectables);

        if (frontRaycast)
        {
            if (frontRaycast.collider != null)
            {
                if(frontRaycast.collider.name == "Crate")
                {
                    gameWorld.detectedCover = new Cover(frontRaycast.collider.gameObject, frontRaycast.transform.position);

                    if (gameWorld.detectedCover != null)
                    {       
                        companion2D.enabled = false;

                        Vector2 cover = new Vector2(frontRaycast.transform.position.x - 1, transform.position.y);

                        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), cover, 3 * Time.deltaTime);
                   }
                    
                    Debug.DrawLine(xStart.position, xEnd.position, Color.green);
                    Debug.Log("Companion Has Detected Ahead Of Itself: " + frontRaycast.collider.name);
                }               

                if(frontRaycast.collider.name == "Zombie")
                {
                    Debug.DrawLine(xStart.position, xEnd.position, Color.green);
                    Debug.Log("Companion Has Detected Ahead Of Itself: " + frontRaycast.collider.name);
                }
                
                if(frontRaycast.collider.name == "Health Pack")
                {
                    if(!frontRaycast.collider.transform.IsChildOf(gameObject.transform))
                        companion2D.enabled = false;
                    else if (frontRaycast.collider.transform.IsChildOf(gameObject.transform))
                        companion2D.enabled = true;

                    Vector2 collectableObj = new Vector2(frontRaycast.transform.position.x, transform.position.y);

                    if(!frontRaycast.collider.transform.IsChildOf(gameObject.transform))
                        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), collectableObj, 3 * Time.deltaTime);

                    Debug.DrawLine(xStart.position, xEnd.position, Color.green);
                    Debug.Log("Companion Has Detected Ahead Of Itself: " + frontRaycast.collider.name);
                }
                
            }
        }

        companion2D.enabled = true;

    }



    void FixedUpdate ()
    {
        RaycastCheck();
	}
}

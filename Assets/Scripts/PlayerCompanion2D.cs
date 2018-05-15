using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompanion2D : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;
    public Vector3 scaleNormal;
    public Vector3 scaleFlipped;
    public Vector3 offsetNormal;
    public Vector3 offsetFlipped;
    public Vector3 targetPosition;

    public float intVelocity;
    public float camLerpTime = 0.1f;
    public float followStrength = 15f;


	// Use this for initialization
	void Start ()
    {
        
	}
	
    public void DetectFlip()
    {
        Transform playerScale = player.transform;

        Vector3 companionScale = transform.localScale;

        if(playerScale.localScale.x < 0)
        {
            offset = offsetNormal;
            companionScale.x *= -1;
            transform.localScale = scaleNormal;
        }
        else if (playerScale.localScale.x > 0)
        {
            offset = offsetFlipped;
            companionScale.x *= -1;
            transform.localScale = scaleFlipped;
        }
    }


	// Update is called once per frame
	void FixedUpdate ()
    {
	    if(player)
        {
            DetectFlip();

            Vector3 pos = transform.position;

            Vector3 targetDir = (player.transform.position - pos);

            intVelocity = targetDir.magnitude * followStrength;

            targetPosition = transform.position + (targetDir.normalized * intVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPosition + offset, camLerpTime);

        }
	}
}

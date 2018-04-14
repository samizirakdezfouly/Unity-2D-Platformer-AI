using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    public Transform[] movementPoints;

    public int currentPoint = 0;

    public float reachDistance = 5.0f;

    public enum movementType  {UseTransform, UsePhysics };

    public movementType moveTypes;

    public float speed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        switch (moveTypes)
        {
            case movementType.UseTransform:
                UseTransform();
                break;
            case movementType.UsePhysics:
                UsePhysics();
                break;
        }
	}

    void UseTransform()
    {
        Vector3 direction = movementPoints[currentPoint].position - transform.position;

        Vector3 directionNormalized = direction.normalized;

        transform.Translate(directionNormalized * (speed * Time.fixedDeltaTime));
        
        if(direction.magnitude <= reachDistance)
            currentPoint++;
        if(currentPoint >= movementPoints.Length)
        {
            currentPoint = 0;
        }
    }

    void UsePhysics()
    {
        Vector3 direction = movementPoints[currentPoint].position - transform.position;

        Vector3 directionNormalized = direction.normalized;

        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.velocity = new Vector2(directionNormalized.x * (speed * Time.fixedDeltaTime), rigidbody2D.velocity.y);

        if (direction.magnitude <= reachDistance)
            currentPoint++;
        if (currentPoint >= movementPoints.Length)
        {
            currentPoint = 0;
        }
    }

    void OnDrawGizmos()
    {
        if (movementPoints == null)
            return;
        foreach(Transform movePoint in movementPoints)
        {
            Gizmos.DrawSphere(movePoint.position, reachDistance);
        }
    }
}

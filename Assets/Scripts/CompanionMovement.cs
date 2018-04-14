using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMovement : MonoBehaviour {

    public float followSpeed;

    public float stoppingDistance;

    private Transform followTarget;

    

	// Use this for initialization
	void Start () {

        followTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {



        if(Vector2.Distance(transform.position, followTarget.position) > stoppingDistance)
            transform.position = Vector2.MoveTowards(transform.position, followTarget.position, followSpeed * Time.deltaTime);

	}


}

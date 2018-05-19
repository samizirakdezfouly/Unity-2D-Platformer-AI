using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float speed;
    public float flipTime;
    public float chargeBuildUp;
    public float timer = 0.0f;

    //public EdgeCollider2D damageColl;


    public PlayerHealth playerHealth;


    private Rigidbody2D enemyRb;
    private Animator enemyAnimator;
    private float nextFlipChance = 0f;
    private bool playerDisabled = false;
    private bool canFlip = true;
    private bool facingRight = false;
    private bool charging;
    private bool inFOV = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Patrol()
    {

    }

}

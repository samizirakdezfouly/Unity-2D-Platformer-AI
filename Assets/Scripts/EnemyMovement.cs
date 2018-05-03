using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed;

    public float flipTime;

    public float chargeBuildUp;

    public float timer = 0.0f;



    private float nextFlipChance = 0f;

    private Animator enemyAnimator;

    private bool canFlip = true;

    private bool facingRight = false;

    private bool charging;

    private bool inFOV = false;

    private Rigidbody2D enemyRb;

    
    void Start ()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody2D>();
	}
	

	void Update ()
    {
        if(inFOV == true)
            timer += Time.deltaTime;
        else
            timer = 0.0f;

        if (Time.time > nextFlipChance)
            if (Random.Range(0, 10) >= 5)
            {
                Flip();
                nextFlipChance = Time.time + flipTime;
            }                     
	}

    void FixedUpdate()
    {
        if(inFOV == true)
        {           
            if (chargeBuildUp < timer)
            {
                Debug.Log("Please");
                if (!facingRight)
                {
                    enemyRb.AddForce(new Vector2(-1, 0) * speed);
                }
                else
                {
                    enemyRb.AddForce(new Vector2(1, 0) * speed);
                }
                enemyAnimator.SetBool("isCharging", true);
            }
        }     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            inFOV = true;
            if(facingRight && other.transform.position.x < transform.position.x)
            {
                Flip();
            }
            else if(!facingRight && other.transform.position.x > transform.position.x)
            {
                Flip();
            }

            canFlip = false;
            charging = true;
            enemyAnimator.SetBool("isReadyToCharge", true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            inFOV = false;
            canFlip = true;
            charging = false;
            enemyRb.velocity = new Vector2(0, 0);
            enemyAnimator.SetBool("isCharging",false);
            enemyAnimator.SetBool("isReadyToCharge", false);
        }
    }

    void Flip()
    {
        if(!canFlip)
        {
            return;
        }

        Vector3 enemyScale = transform.localScale;

        enemyScale.x *= -1;

        transform.localScale = enemyScale;

        facingRight = !facingRight;
    }

}

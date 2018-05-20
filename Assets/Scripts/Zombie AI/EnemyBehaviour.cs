using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float patrolSpeed;
    //public float flipTime;
    public float chargeBuildUp;
    public float timer = 0.0f;

    public int attackDamage;

    public float chargeSpeed;

    public PlayerHealth playerHealth;

    public int health;

    private Rigidbody2D enemyRb;
    private Animator enemyAnimator;
    private float nextFlipChance = 0f;
    private bool playerDisabled = false;
    private bool canChangeDirection = true;
    private bool facingRight = false;
    private bool charging;
    private bool inFOV = false;
    public static bool facingDefault;
    private bool isStunned;

    public Rigidbody2D player;

    public Vector2 knockbackForce;
    public SpriteRenderer[] playerBody;

    public float durationOfKnockback;

    private float knockbackTimer = 0.0f;

    private IEnemyStates currentEnemyState;
    private EnemySensor enemySensor;

    void Start ()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemySensor = GetComponent<EnemySensor>();
        enemyAnimator = GetComponent<Animator>();
        isStunned = false;
        ChangeEnemyState(new PatrolState());
	}

    public void TakeDamage(int damage)
    {
        health -= damage;

        isStunned = true;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeCompanionDamage(int damage)
    {
        this.health -= damage;

        //isStunned = true;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update ()
    {
        currentEnemyState.ExecuteState();

        if(inFOV)
        {
            timer += Time.deltaTime;
        }

        if(isStunned)
        {
            knockbackTimer += Time.deltaTime;
            {
                if (knockbackTimer < durationOfKnockback && isStunned)
                {
                    foreach (SpriteRenderer sprite in playerBody)
                    {
                        sprite.color = Color.red;
                    }

                    player.GetComponent<PlayerMovement>().enabled = false;
                    player.AddForce(knockbackForce, ForceMode2D.Impulse);
                }
                else
                {
                    foreach (SpriteRenderer sprite in playerBody)
                    {
                        sprite.color = Color.white;
                    }
                    knockbackTimer = 0.0f;
                    isStunned = false;
                    player.GetComponent<PlayerMovement>().enabled = true;
                }
            }
        }

        if (playerHealth.health == 0)
        {
            ChangeEnemyState(new PatrolState());
        }
    }

    public void ChangeEnemyState(IEnemyStates newState)
    {
        if (currentEnemyState != null)
        {
            currentEnemyState.OnStateExit();
        }

        currentEnemyState = newState;

        currentEnemyState.OnStateEnter(this,enemySensor);
    }

    public void Patrol()
    {
        if (!enemySensor.patrolDetection || enemySensor.patrolDetection.collider.name == "Crate")
            ChangeDirection();

        transform.Translate(GetDirection() * (patrolSpeed * Time.deltaTime));
    }

    public void EngageEnemy()
    {
        if(enemySensor.playerDetection.collider == null)
        {
            timer = 0.0f;
            ChangeEnemyState(new PatrolState());
        }

        if(enemySensor.playerDetection.collider.name == "Player")
        {
            inFOV = true;

            if (facingDefault && enemySensor.playerDetection.collider.transform.position.x < transform.position.x)
            {
                ChangeDirection();
            }
            else if(!facingDefault && enemySensor.playerDetection.collider.transform.position.x > transform.position.x)
            {
                ChangeDirection();
            }

            canChangeDirection = false;
            charging = true;
            enemyAnimator.SetBool("isReadyToCharge", true);


            if (chargeBuildUp < timer && isStunned == false)
            {
                enemySensor.damagePlayerEnabled = true;

                if(facingDefault)
                {
                    enemyRb.AddForce(GetDirection() * chargeSpeed);
                }
                else if(!facingDefault)
                {
                    enemyRb.AddForce(GetDirection()* chargeSpeed);
                }

                enemyAnimator.SetBool("isCharging", true);

                if(enemySensor.playerDamage.collider != null && enemySensor.playerDamage.collider.name == "Player")
                {
                    isStunned = true;

                    enemySensor.playerDamage.collider.SendMessage("TakeDamage", attackDamage);
                }
            }
            else
            {
                enemySensor.damagePlayerEnabled = false;
            }
        }
    }

    //public void KnockBackPlayer()
    //{
    //  if (knockbackTimer < durationOfKnockback && isStunned)
    //        {
    //            foreach (SpriteRenderer sprite in playerBody)
    //            {
    //                sprite.color = Color.red;
    //            }

    //            player.GetComponent<PlayerMovement>().enabled = false;
    //            player.AddForce(knockbackForce, ForceMode2D.Impulse);
    //        }
    //        else
    //        {
    //            foreach (SpriteRenderer sprite in playerBody)
    //            {
    //                sprite.color = Color.white;
    //            }
    //            knockbackTimer = 0.0f;
    //            isStunned = false;
    //            player.GetComponent<PlayerMovement>().enabled = true;
    //        }
    //}

    public Vector2 GetDirection()
    {
        return facingDefault ? Vector2.right : Vector2.left;
    }

    public void ChangeDirection()
    {
        if (!canChangeDirection)
            return;

        facingDefault = !facingDefault;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
    }

    public void ResetEnemy()
    {
        inFOV = false;
        canChangeDirection = true;
        charging = false;
        enemyRb.velocity = new Vector2(0, 0);
        enemyAnimator.SetBool("isCharging", false);
        enemyAnimator.SetBool("isReadyToCharge", false);
        //ChangeDirection();
    }

}

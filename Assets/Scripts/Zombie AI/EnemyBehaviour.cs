using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    [Header("Player References")]
    public PlayerHealth playerHealth;
    public Rigidbody2D player;
    public SpriteRenderer[] playerBody;

    [Header("Patrol Settings")]
    public float patrolSpeed;

    [Header("Charge Settings")]
    public Vector2 knockbackForce;
    public float durationOfKnockback;
    public float chargeBuildUp;
    public float chargeSpeed;
    public int attackDamage;

    public int zombieNumber;

    private bool facingDefault;

    private float timer = 0.0f;
    private float knockbackTimer = 0.0f;

    private Rigidbody2D enemyRb;
    private Animator enemyAnimator;

    private bool canChangeDirection = true;
    private bool charging;
    private bool inFOV = false;
    private bool isStunned;  

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
        if (enemySensor.patrolDetection == false || enemySensor.patrolDetection.collider.name == "Crate" || enemySensor.patrolDetection.collider.name == "Zombie")
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

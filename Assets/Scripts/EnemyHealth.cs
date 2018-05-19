using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;

    private Rigidbody2D rb;

    public Vector2 knockbackForce;

    public SpriteRenderer[] enemyBody;

    public float durationOfKnockback;

    private float knockbackTimer = 0.0f;

    private bool isStunned;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        isStunned = true;

        if(health <= 0)
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

    void Update()
    {
        if (isStunned)
        {
            knockbackTimer += Time.deltaTime;

            if (knockbackTimer < durationOfKnockback && isStunned)
            {
                foreach (SpriteRenderer sprite in enemyBody)
                {
                    sprite.color = Color.red;
                }

                rb.GetComponent<EnemyMovement>().enabled = false;
                rb.AddForce(knockbackForce, ForceMode2D.Impulse);
            }

            else
            {
                foreach (SpriteRenderer sprite in enemyBody)
                {
                    sprite.color = Color.white;
                }
                knockbackTimer = 0.0f;
                isStunned = false;
                rb.GetComponent<EnemyMovement>().enabled = true;
            }
        }
    }
}


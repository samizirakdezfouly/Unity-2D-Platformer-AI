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

    private EnemyBehaviour behaviour;

    private EnemySensor sensor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        behaviour = GetComponent<EnemyBehaviour>();
        sensor = GetComponent<EnemySensor>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        isStunned = true;

        if (!sensor.playerDetection)
            behaviour.ChangeDirection();

        if(health <= 0)
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
            }
        }
    }
}


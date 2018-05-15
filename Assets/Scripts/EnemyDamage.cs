using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    public EdgeCollider2D damageCollider;

    public BoxCollider2D detectionCollider;

    public Vector2 knockbackForce;

    public Rigidbody2D player;

    public SpriteRenderer [] playerBody;

    public float durationOfKnockback;

    private float knockbackTimer = 0.0f;

    private bool isStunned;
	
	void Start ()
    {
        damageCollider.enabled = false;
        isStunned = false;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isStunned = true;
        }
    }

    void Update()
    {
        if (isStunned)
        {
            knockbackTimer += Time.deltaTime;

            if(knockbackTimer < durationOfKnockback && isStunned)
            {
                foreach(SpriteRenderer sprite in playerBody)
                {
                    sprite.color = Color.red;
                }

                player.GetComponent<PlayerMovement>().enabled = false;
                player.AddForce(knockbackForce,ForceMode2D.Impulse);
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

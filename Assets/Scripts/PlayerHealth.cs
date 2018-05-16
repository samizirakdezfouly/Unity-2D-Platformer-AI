using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public Text healthText;

    public Text playerStatusText;

    public int health = 100;

    private PlayerMovement abilityToMove;

    private CapsuleCollider2D playerDetectCollider;

    private Animator anim;
 
	void Start ()
    {
        abilityToMove = GetComponent<PlayerMovement>();
        playerDetectCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void GainHealth(int amount)
    {
        health += amount;
    }

    void Update ()
    {
        CheckStatus();

        healthText.text = "Health: " + health;
	}

    public void CheckStatus()
    {
        if (health <= 0)
        {
            health = 0;
            abilityToMove.enabled = false;
            //playerDetectCollider.enabled = false;
            playerStatusText.text = "Player Status: Needs Assistance";
            anim.SetBool("disabled", true);
        }

        if (health > 0)
        {
            playerStatusText.text = "Player Status: Normal";
            abilityToMove.enabled = true;
            //playerDetectCollider.enabled = true;
        }

        if (health > 100)
        {
            health = 100;
        }
    }
}

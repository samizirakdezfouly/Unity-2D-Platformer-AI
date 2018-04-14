using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D playerRigidbody;

    private Animator playerAnimator;

    public GameObject weapon;

    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask whatIsGround;

    private bool facingDefault;

    private bool attacking;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private bool airControl;

    private static float playersMoveInput;

	void Start ()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
	}
	
	
    void Update()
    {
        HandleInput();
    }

	void FixedUpdate ()
    {

        float horizontal = Input.GetAxis("Horizontal");

        playersMoveInput = horizontal;

        isGrounded = IsGrounded();

        Movement(horizontal);

        FlipPlayer(horizontal);

        Attacks();

        ResetValues();

	}

    private void Movement(float horizontal)
    {
        if(playerRigidbody.velocity.y < 0)
        {
            playerAnimator.SetBool("land", true);
        }

        if(!this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, playerRigidbody.velocity.y);
        }

        if(isGrounded && jump)
        {
            isGrounded = false;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAnimator.SetBool("jump", true);
        }

        playerAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void FlipPlayer(float horizontal)
    {
        if(horizontal > 0 && !facingDefault || horizontal < 0 && facingDefault)
        {
            facingDefault = !facingDefault;

            Vector3 playerScale = transform.localScale;

            playerScale.x *= -1;

            transform.localScale = playerScale;

        }
    }

    private void Attacks()
    {
        if(attacking && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            //weapon.SetActive(true);
            playerAnimator.SetTrigger("meleeAttack");
            playerRigidbody.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attacking = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private bool IsGrounded()
    {
        if(playerRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i = 0; i< colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        playerAnimator.SetBool("jump", false);
                        playerAnimator.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        //playerAnimator.SetBool("land", true);
        return false;
    }

    private void ResetValues()
    {
        attacking = false;
        jump = false;
        //weapon.SetActive(false);
    }

}

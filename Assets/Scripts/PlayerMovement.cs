using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D playerRigidbody;

    private Animator playerAnimator;

    private float attackCoolDown = 0.2f;

    public Collider2D attackTrigger;

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

    public bool attacking = false;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private bool airControl;

    private static float playersMoveInput;

	void Start ()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        attackTrigger.enabled = false;
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

        if(isGrounded || airControl)
        {
            if (isGrounded && playerRigidbody.velocity.x > 0.3 || isGrounded && playerRigidbody.velocity.x < -0.3)
            {
                SoundManager.PlaySound("PlayerRun");
                //StartCoroutine(Delay(1.0f));
            }
                

            playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, playerRigidbody.velocity.y);
        }

        if(isGrounded && jump)
        {
            isGrounded = false;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAnimator.SetBool("jump", true);
            SoundManager.PlaySound("PlayerJump");
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
        if (attacking && isGrounded)
        {
            playerAnimator.SetBool("meleeAttack", true);
            attackTrigger.enabled = true;

            StartCoroutine(DelayMelee(attackCoolDown));                
        }
    }

    IEnumerator DelayMelee(float delayTime)
    {
        yield return null;
        yield return new WaitForSeconds(delayTime);
        attackTrigger.enabled = false;
        playerAnimator.SetBool("meleeAttack", false);
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
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
    }

}

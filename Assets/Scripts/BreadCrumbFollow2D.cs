using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadCrumbFollow2D : MonoBehaviour {

    private Rigidbody2D rb2D;

    private Animator companionAnimator;

    private bool facingDefault = false;

    public GameObject player;

    public float movementSpeed = 6.0f;
	// Use this for initialization
	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();

        companionAnimator = GetComponent<Animator>();
	}

    private void FlipPlayer(float horizontal)
    {
        if (horizontal > 0 && !facingDefault || horizontal < 0 && facingDefault)
        {
            facingDefault = !facingDefault;

            Vector3 playerScale = transform.localScale;

            playerScale.x *= -1;

            transform.localScale = playerScale;

        }
    }

    // Update is called once per frame
    void Update () {

        float horizontal = Input.GetAxis("Horizontal");

        FlipPlayer(horizontal);

        rb2D.velocity = new Vector2(horizontal * movementSpeed, rb2D.velocity.y);

        companionAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
}

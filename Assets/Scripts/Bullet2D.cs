using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour {

    public CompanionBehaviour companion;

    public float bulletSpeed = 50.0f;

    public float bulletDeathTime = 1.5f;

    public int bulletDamage;

    private Rigidbody2D bullet;


    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();

        Invoke("BulletDeath", bulletDeathTime);
    }

    void BulletDeath()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        CancelInvoke("BulletDeath");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemies")
        {
            collision.SendMessage("TakeCompanionDamage", bulletDamage);
            Destroy(gameObject);
        }
    }


    void FixedUpdate()
    {
        //bullet.velocity = transform.right * bulletSpeed;

        switch (companion.isScaled)
        {
            case true:
                //bullet.AddForce(new Vector2(bulletSpeed, 0));
                //bullet.velocity = Vector2.left * bulletSpeed;
                break;
            case false:
                //bullet.AddForce(new Vector2(bulletSpeed, 0));
                bullet.velocity = Vector2.right * bulletSpeed;
                break;
        }
    }
}

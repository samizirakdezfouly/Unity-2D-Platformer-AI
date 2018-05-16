using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour {

    private float bulletSpeed = 7.0f;

    private float bulletDeathTime = 1.5f;

    public int bulletDamage;

    public string tagToDamage;

    public string playerTag;

    public string cantShootTag;

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
        if (collision.CompareTag(tagToDamage))
        {
            collision.SendMessage("TakeDamage", bulletDamage);
        }

        if (collision.tag != playerTag && collision.tag != cantShootTag)
            Destroy(gameObject);
    }


    void Update()
    {
        bullet.velocity = transform.forward * bulletSpeed;

        if (Gun2D.isFlipped == true)
            bullet.AddForce(new Vector2(-bulletSpeed, 0));
        else if (Gun2D.isFlipped == false)
            bullet.AddForce(new Vector2(bulletSpeed, 0));
    }
}

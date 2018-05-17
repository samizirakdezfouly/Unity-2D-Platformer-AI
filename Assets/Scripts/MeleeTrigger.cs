using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTrigger : MonoBehaviour {

    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemies" || collision.tag == "Destructable")
        {
            collision.SendMessage("TakeDamage", damage);
        }
    }

}

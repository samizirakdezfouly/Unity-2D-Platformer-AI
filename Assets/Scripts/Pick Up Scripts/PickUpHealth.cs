using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : MonoBehaviour {

    public int healthValue;

    public Collider2D trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.SendMessage("GainHealth", healthValue);
            Destroy(gameObject);
        }

        if(collision.tag == "Companion")
        {
            gameObject.transform.parent = collision.gameObject.transform;
            //trigger.enabled = false;
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

    public int ammoValue;

    public CompanionBehaviour companion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Companion")
        {
            collision.SendMessage("PickUpAmmo", ammoValue);
            Destroy(gameObject);
        }

        if(collision.tag == "Player")
        {
            companion.SendMessage("PickUpAmmo", ammoValue);
            Destroy(gameObject);
        }
    }
}

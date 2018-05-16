using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDrops : MonoBehaviour {

    public GameObject[] possibleDrops;

    public Sprite [] crateSprites;

    public int health = 30;

    private int dropRNGValue;

    public int generatedRNGValue;

    private SpriteRenderer sr;

    // Use this for initialization
    void Start ()
    {
        dropRNGValue = possibleDrops.Length;
        sr = GetComponent<SpriteRenderer>();
	}

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(health == 20)
        {
            sr.sprite = crateSprites[0];
        }

        if(health == 10)
        {
            sr.sprite = crateSprites[1];
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    void OnDestroy()
    {
        generatedRNGValue = Random.Range(0, dropRNGValue);

        Debug.Log("" + generatedRNGValue);

        Instantiate(possibleDrops[generatedRNGValue], transform.position,Quaternion.Euler(0,0,0));

    }

}

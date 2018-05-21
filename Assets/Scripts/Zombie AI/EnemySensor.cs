using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour {

    public RaycastHit2D playerDetection;
    public RaycastHit2D playerDamage;
    public RaycastHit2D patrolDetection;

    public Transform playerDetectionStart, playerDetectionEnd, 
                        playerDamageStart, playerDamageEnd, 
                            patrolDetectionStart, patrolDetectionEnd;

    private int patrolSensorDetectables;
    private int playerSensorDetectables;

    public bool damagePlayerEnabled = false;

	void Start ()
    {
        patrolSensorDetectables = LayerMask.GetMask("Cover", "Platforms","Enemy");
        playerSensorDetectables = LayerMask.GetMask("Player");

        playerDetection = Physics2D.Linecast(playerDetectionStart.position, playerDetectionEnd.position, playerSensorDetectables);
        playerDamage = Physics2D.Linecast(playerDamageStart.position, playerDamageEnd.position, playerSensorDetectables);
        patrolDetection = Physics2D.Linecast(patrolDetectionStart.position, patrolDetectionEnd.position, patrolSensorDetectables);

	}
	
    public void PatrolCheck()
    {
        patrolDetection = Physics2D.Linecast(patrolDetectionStart.position, patrolDetectionEnd.position, patrolSensorDetectables);

        if (patrolDetection)
        {
            Debug.DrawLine(patrolDetectionStart.position, patrolDetectionEnd.position, Color.red);
        }
    }

    public void DamageCheck()
    {
        playerDamage = Physics2D.Linecast(playerDamageStart.position, playerDamageEnd.position, playerSensorDetectables);

        if(playerDamage)
        {
            Debug.DrawLine(playerDamageStart.position, playerDamageEnd.position, Color.green);
        }
    }

    public void PlayerDetectionCheck()
    {
        playerDetection = Physics2D.Linecast(playerDetectionStart.position, playerDetectionEnd.position, playerSensorDetectables);

        if(playerDetection)
        {
            Debug.DrawLine(playerDetectionStart.position, playerDetectionEnd.position, Color.red);
        }
    }

	void FixedUpdate () {

        if (damagePlayerEnabled)
            DamageCheck();

        PatrolCheck();

        PlayerDetectionCheck();
        
	}
}

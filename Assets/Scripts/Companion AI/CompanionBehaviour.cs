using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionBehaviour : MonoBehaviour {

    [Header("Follow Player Settings")]
    public GameObject player;

    public Vector3 scaleNormal;
    public Vector3 scaleFlipped;
    public Vector3 offsetNormal;
    public Vector3 offsetFlipped;
    public Vector3 targetPosition;

    public float camLerpTime = 0.1f;
    public float followStrength = 30.0f;

    private float intVelocity;
    private Vector3 offset;

    [Header("Scavanging Settings")]
    public GameObject companionWeapon;

    private bool isFlipped = false;

    [Header("Attack Enemies Settings")]
    public GameObject bulletToFire;

    public Transform bulletSpawnLocation;

    public Vector2 bulletSpawnRotation;

    public int ammo;

    public Text ammoCountUI;

    public Text companionStatus;

    private bool canFire = true;

    ///

    private ICompanionStates currentCompanionState;
    private CompanionSensor companionSensor;
    public bool shouldChangeState = true;

    void Start ()
    {
        companionSensor = GetComponent<CompanionSensor>();
        ChangeCompanionState(new FollowPlayerState());
	}
	
	void FixedUpdate ()
    {
        currentCompanionState.ExecuteState();
	}

    public void ChangeCompanionState(ICompanionStates newState)
    {
        if(currentCompanionState != null)
        {
            currentCompanionState.OnStateExit();
        }

        currentCompanionState = newState;
        currentCompanionState.OnStateEnter(this,companionSensor);
    }



    public void FollowPlayer()
    {
        if (player)
        {
            companionStatus.text = "Companion Status: Following Player";

            DetectFlip();

            Vector3 pos = transform.position;

            Vector3 targetDir = (player.transform.position - pos);

            intVelocity = targetDir.magnitude * followStrength;

            targetPosition = transform.position + (targetDir.normalized * intVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPosition + offset, camLerpTime);
        }
    }

    public void ScavangeItem()
    {
        if(companionSensor.frontRaycast.collider != null)
        {
            companionStatus.text = "Companion Status: Scavanging";

            companionWeapon.SetActive(false);

            Vector2 collectableObj = new Vector2(companionSensor.frontRaycast.transform.position.x, transform.position.y);

            if (!companionSensor.frontRaycast.collider.transform.IsChildOf(gameObject.transform))
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), collectableObj, 3 * Time.deltaTime);

            if (companionSensor.frontRaycast.collider.transform.IsChildOf(gameObject.transform))
            {
                if (!isFlipped)
                {
                   FlipReversed();
                   isFlipped = true;
                }

                Vector2 playerLocation = new Vector2(player.transform.position.x, transform.position.y);

                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), playerLocation, 3 * Time.deltaTime);
            }
        }
        else
        {
            ChangeCompanionState(new FollowPlayerState());
            companionWeapon.SetActive(true);
            isFlipped = false;
        }
    }

    public void GetBehindCover()
    {
        if (companionSensor.frontRaycast.collider != null)
        {
            companionStatus.text = "Companion Status: Behind Cover";

            Vector2 cover = new Vector2(companionSensor.frontRaycast.transform.position.x - 1, transform.position.y);

            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), cover, 3 * Time.deltaTime);
        }
        else
        {
            ChangeCompanionState(new FollowPlayerState());
        }
    }

    public void EngageEnemy()
    {
        if (companionSensor.frontRaycast.collider != null && ammo > 0)
        {
            shouldChangeState = true;

            companionStatus.text = "Companion Status: Engaging Enemy";

            Vector2 enemy = new Vector2(companionSensor.frontRaycast.transform.position.x + 3, transform.position.y);

            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), enemy, 3 * Time.deltaTime);

            if (canFire)
            {
                canFire = false;

                ammo--;

                ammoCountUI.text = "Ammo: " + ammo;

                Instantiate(bulletToFire, bulletSpawnLocation.position, Quaternion.Euler(bulletSpawnRotation));

                StartCoroutine(FiringRate(0.5f));
            }
        }
        else
        {
            if (shouldChangeState)
            {
                shouldChangeState = false;
                ChangeCompanionState(new FollowPlayerState());
            }
        }
    }


    public void PickUpAmmo(int amount)
    {
        ammo += amount;
        ammoCountUI.text = "Ammo: " + ammo;
    }

    IEnumerator FiringRate(float interval)
    {
        Debug.Log("Help");
        yield return new WaitForSeconds(interval);
        canFire = true;
    }

    public void DetectFlip()
    {
        Transform playerScale = player.transform;

        Vector3 companionScale = transform.localScale;

        if (playerScale.localScale.x < 0)
        {
            offset = offsetNormal;
            transform.localScale = scaleNormal;
        }
        else if (playerScale.localScale.x > 0)
        {
            offset = offsetFlipped;
            transform.localScale = scaleFlipped;
        }
    }

    public void FlipReversed()
    {
        Vector3 companionScale = transform.localScale;

        Vector3 playerPosition = player.transform.position;

        bool scaleChanged = false;

        if(playerPosition.x > gameObject.transform.position.x && !scaleChanged)
        {
            companionScale.x = -0.5f;
            scaleChanged = true;
        }

        if(playerPosition.x < gameObject.transform.position.x && !scaleChanged)
        {
            companionScale.x = 0.5f;
            scaleChanged = true;
        }

        transform.localScale = companionScale;
    }
}

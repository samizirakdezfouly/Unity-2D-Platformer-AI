using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun2D : MonoBehaviour {

    public GameObject bulletToFire;
    public Text ammoCountUI;
 
    public int currentAmmo;
    public int maxAmmo;
    public float damage;
    public float impactForce;
    public float reloadTime;

    private Transform characterPosition;
    public Transform bulletSpawnPoint;

    private string firingBoolName;

    private float gunFireInterval = 0.5f;

    private Vector3 bulletSpawnRotation = new Vector3(0, 0, 0);

    private bool gunHasSound = false;

    private Animator characterAnimator;
    private static bool isFiring = false;
    private static bool isReloading = false;
    public static bool isFlipped = false;


    void Start()
    {
        characterPosition = GetComponent<Transform>();

        characterAnimator = GetComponent<Animator>();

        currentAmmo = maxAmmo;

        ammoCountUI.text = currentAmmo + "/" + maxAmmo;
    }

    void ResetGun()
    {
        isFiring = false;
    }

    void ShootGun()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            return;
        }

        isFiring = true;

        Instantiate(bulletToFire, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnRotation));

        currentAmmo--;

        ammoCountUI.text = "Ammo: " + currentAmmo + "/" + maxAmmo;

        Invoke("ResetGun", gunFireInterval);

        characterAnimator.SetBool(firingBoolName, true);

        StartCoroutine(DelayAnimationMethod(0.5f));

    }

    void PickUpAmmo(int ammount)
    {
        currentAmmo += ammount;
    }

    IEnumerator DelayAnimationMethod(float delayTime)
    {
        yield return null;
        yield return new WaitForSeconds(delayTime);
        characterAnimator.SetBool(firingBoolName, false);
    }

    IEnumerator Reload(int reloadAmmount)
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = reloadAmmount;

        isReloading = false;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
            if (!isFiring)
                ShootGun();

        if (characterPosition.localScale.x < 0 && !isFiring)
            isFlipped = true;
        else if (characterPosition.localScale.x > 0 && !isFiring)
            isFlipped = false;

    }
}

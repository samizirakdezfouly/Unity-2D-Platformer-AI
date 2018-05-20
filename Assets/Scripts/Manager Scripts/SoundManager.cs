using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip playerRun, playerJump;

    static AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        playerRun = Resources.Load<AudioClip>("playerRun");
        playerJump = Resources.Load<AudioClip>("playerJump");
    }

    public static void PlaySound(string audioClipName)
    {
        switch (audioClipName)
        {
            case "PlayerRun":
                if (audioSource.isPlaying)
                    break;
                    audioSource.PlayOneShot(playerRun);
                    break;
            case "PlayerJump":
                audioSource.PlayOneShot(playerJump);
                break;
        }
    }
}

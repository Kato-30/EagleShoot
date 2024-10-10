using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource backgroundSource;
    public AudioSource sfxSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip eagleFlySound;
    public AudioClip eagleDieSound;
    public AudioClip backgroundMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        PlayBgMusic();
    }

    void PlayBgMusic()
    {
        backgroundSource.clip = backgroundMusic;
        backgroundSource.Play();
    }

    public void PlayShootSound()
    {
        sfxSource.PlayOneShot(shootSound);
    }

    public void PlayReloadSound()
    {
        sfxSource.PlayOneShot(reloadSound);
    }


    public void PlayEagleFlySound()
    {
        sfxSource.PlayOneShot(eagleFlySound);
    }

    public void PlayEagleDieSound()
    {
        sfxSource.PlayOneShot(eagleDieSound);
    }
}

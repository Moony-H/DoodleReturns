using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonGameObject<SoundManager>
{
    public AudioSource audioSource;
    public AudioSource bgmSource;

    public AudioClip HammerSound;
    public AudioClip JumpSound;
    public AudioClip StepSound;
    public AudioClip Spring;
    public AudioClip DoorSound;
    public AudioClip MakeSound;
    public AudioClip SliceSound;
    public AudioClip BGM;

    public void Start()
    {
        var managers = FindObjectsOfType<SoundManager>();
        if (managers.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayStepSound()
    {
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(StepSound);
    }
    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(JumpSound);
    }
    public void PlayHammerSound()
    {
        audioSource.PlayOneShot(HammerSound);
    }
    
    public void PlaySpringSound()
    {
        audioSource.PlayOneShot(Spring);
    }
    public void PlayDoorSound()
    {
        audioSource.PlayOneShot(DoorSound);
    }

    public void PlayMakeSound()
    {
        audioSource.PlayOneShot(MakeSound);
    }

    public void PlaySliceSound()
    {
        audioSource.PlayOneShot(SliceSound);
    }

    public static void PlayBGM()
    {
        Instance.bgmSource.clip = Instance.BGM;
        Instance.bgmSource.Play();
    }

    public static void PauseBGM()
    {
        Instance.bgmSource.Pause();
    }

    public static void UnPauseBGM()
    {
        Instance.bgmSource.UnPause();
    }

    public static void StopBGM()
    {
        Instance.bgmSource.Stop();
    }


}

using System;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    OptionsStatus optionsStatus;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioClip gunshot;
    [SerializeField]
    AudioClip HamDamage;
    [SerializeField]
    AudioClip HamDeath;
    [SerializeField]
    AudioClip outOfAmmo;
    [SerializeField]
    AudioClip Die;
    [SerializeField]
    AudioClip Heal;
    [SerializeField]
    AudioClip TakeDamage;

    // logs 
    [SerializeField]
    AudioClip Hamaoke;
    [SerializeField]
    AudioClip HamFightEntry;
    [SerializeField]
    AudioClip HamLuca;
    [SerializeField]
    AudioClip hamming;
    [SerializeField]
    AudioClip Pears;
    [SerializeField]
    AudioClip AllHam;
    [SerializeField]
    AudioClip Victim1;
    [SerializeField]
    AudioClip Victim2;
    [SerializeField]
    AudioClip Wailing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        optionsStatus = FindAnyObjectByType<OptionsStatus>();
        SetSFXVolume(optionsStatus.getSFXMultiplier());
        SetMusicVolume(optionsStatus.getMusicMultiplier());
        musicSource.Play();
    }

    public void playHamaoke()
    {
        stopCurrent();
        audioSource.PlayOneShot(Hamaoke, 1.2f);
    }

    public void playHamFightEntry()
    {
        stopCurrent();
        audioSource.PlayOneShot(HamFightEntry, 1.2f);
    }

    public void playHamLuca()
    {
        stopCurrent();
        audioSource.PlayOneShot(HamLuca, 2f);
    }

    public void playHamming()
    {
        stopCurrent();
        audioSource.PlayOneShot(hamming, 1.2f);
    }

    public void playPears()
    {
        stopCurrent();
        audioSource.PlayOneShot(Pears, 1.2f);
    }

    public void playAllHam()
    {
        stopCurrent();
        audioSource.PlayOneShot(AllHam, 1.2f);
    }

    public void playVictim1()
    {
        stopCurrent();
        audioSource.PlayOneShot(Victim1, 1.3f);
    }

    public void playVictim2()
    {
        stopCurrent();
        audioSource.PlayOneShot(Victim2, 1.2f);
    }

    public void playWailing()
    {
        stopCurrent();
        audioSource.PlayOneShot(Wailing, 1.2f);
    }

    public void playGunshot()
    {
        audioSource.PlayOneShot(gunshot, 0.9f);
    }

    public void playHamDamage()
    {
        audioSource.PlayOneShot(HamDamage);
    }

    public void playHamDeath()
    {
        audioSource.PlayOneShot(HamDeath);
    }

    public void playOurOfAmmo()
    {
        audioSource.PlayOneShot(outOfAmmo);
    }

    public void playDie()
    {
        audioSource.PlayOneShot(Die);
    }

    public void playHeal()
    {
        audioSource.PlayOneShot(Heal);
    }

    public void playTakeDamage()
    {
        audioSource.PlayOneShot(TakeDamage);
    }

    public AudioSource getAudioSource()
    {
        return audioSource;
    }

    public void SetSFXVolume(float volumeIn)
    {
        audioSource.volume = volumeIn;
        Debug.Log("volume in:"+volumeIn+" + "+audioSource.volume);
    }

    public void SetMusicVolume(float volumeIn)
    {
        musicSource.volume = volumeIn;
    }

    void stopCurrent()
    {
        audioSource.Pause();
    }
}
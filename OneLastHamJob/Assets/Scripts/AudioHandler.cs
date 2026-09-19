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
        audioSource.PlayOneShot(Hamaoke, 1.2f);
    }

    public void playHamFightEntry()
    {
        audioSource.PlayOneShot(HamFightEntry, 1.2f);
    }

    public void playHamLuca()
    {
        audioSource.PlayOneShot(HamLuca, 1.2f);
    }

    public void playHamming()
    {
        audioSource.PlayOneShot(hamming, 1.2f);
    }

    public void playPears()
    {
        audioSource.PlayOneShot(Pears, 1.2f);
    }

    public void playAllHam()
    {
        audioSource.PlayOneShot(AllHam, 1.2f);
    }

    public void playVictim1()
    {
        audioSource.PlayOneShot(Victim1, 1.2f);
    }

    public void playVictim2()
    {
        audioSource.PlayOneShot(Victim2, 1.2f);
    }

    public void playWailing()
    {
        audioSource.PlayOneShot(Wailing, 1.2f);
    }

    public void playGunshot()
    {
        audioSource.PlayOneShot(gunshot);
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
}
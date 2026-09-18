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
    AudioClip outOfAmmo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        optionsStatus = FindAnyObjectByType<OptionsStatus>();
        SetSFXVolume(optionsStatus.getSFXMultiplier());
        SetMusicVolume(optionsStatus.getMusicMultiplier());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playGunshot()
    {
        audioSource.PlayOneShot(gunshot);
        Debug.Log(audioSource.volume +" v");
    }

    public void playOurOfAmmo()
    {
        audioSource.PlayOneShot(outOfAmmo);
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
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        optionsStatus = FindAnyObjectByType<OptionsStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGunshot()
    {   
        Debug.Log("change to set volume instead");
        audioSource.volume = optionsStatus.getSFXMultiplier();
        audioSource.PlayOneShot(gunshot);
    }

    public AudioSource getAudioSource()
    {
        return audioSource;
    }
}

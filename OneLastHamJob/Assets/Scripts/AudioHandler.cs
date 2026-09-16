using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioClip gunshot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGunshot()
    {
        audioSource.PlayOneShot(gunshot);
    }

    public AudioSource getAudioSource()
    {
        return audioSource;
    }
}

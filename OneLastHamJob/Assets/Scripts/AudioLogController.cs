using System;
using UnityEngine;

public class AudioLogController : MonoBehaviour
{
    BillboardController interactSign;
    [SerializeField]
    AudioClip audioClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactSign = GetComponentInChildren<BillboardController>();
    }

    public void AudioLogInteract()
    {
        interactSign.preDestroy();
        Destroy(gameObject);
    }

    public AudioClip getAudioClip()
    {
        return audioClip;
    }
}

using UnityEngine;

public class OptionsStatus : MonoBehaviour
{
    float SFXMultiplier=1;
    float MusicMultiplier=1;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public float getSFXMultiplier()
    {
        return SFXMultiplier;
    }

    public float getMusicMultiplier()
    {
        return MusicMultiplier;
    }

    public void setSFXMultiplier(float SFXMultiplierIn)
    {
        SFXMultiplier = SFXMultiplierIn;
    }

    public void setMusicMultiplier(float MusicMultiplierIn)
    {
        MusicMultiplier = MusicMultiplierIn;
    }
}
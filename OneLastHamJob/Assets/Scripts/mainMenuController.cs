using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuController : MonoBehaviour
{
    OptionsStatus optionsHolder;

    [SerializeField]
    GameObject optionsMenu;
    Slider SFXSlider;
    Slider MusicSlider;

    public void onStartButtonPress()
    {
        SceneManager.LoadScene("Level");
    }

    public void onExitButtonPress()
    {
        Application.Quit();
    }

    public void optionsButtonPress()
    {
        optionsMenu.SetActive(true);
        optionsHolder = FindAnyObjectByType<OptionsStatus>();
        Slider[] sliders = FindObjectsByType<Slider>();
        for(int i = 0; i < 2; i++)
        {
            if (sliders[i].name == "SFXSlider")
            {
                SFXSlider = sliders[i];
            }
            else
            {
                MusicSlider = sliders[i];
            }
        }
        
        SetSliderPositions();
    }

    public void optionsButtonBackPress()
    {
        optionsMenu.SetActive(false);
    }

    public void SFXSliderChange()
    {
        optionsHolder.setSFXMultiplier(SFXSlider.value);
    }

    public void MusicSliderChange()
    {
        optionsHolder.setMusicMultiplier(MusicSlider.value);
    }

    void SetSliderPositions()
    {
        SFXSlider.value=optionsHolder.getSFXMultiplier();
        MusicSlider.value=optionsHolder.getMusicMultiplier();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject optionsMenu;
    OptionsStatus optionsHolder;

    Slider SFXSlider;
    Slider MusicSlider;

    [SerializeField]
    GameObject confirmMenu;

    [SerializeField]
    PlayerController playerController;
    InputAction pause;

    bool isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pause = InputSystem.actions.FindAction("Pause");
    }

    void Update()
    {
        if (pause.triggered)
        {
            onPause();
        }
    }

    void onPause()
    {
        isPaused=!isPaused;
        if (isPaused)
        {
            //pause game and load pause menu
            pauseMenu.SetActive(true);
        }
        else
        {
            // play game and remove the currently loaded menu
            pauseMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }
        playerController.setGameSpeed();
    }

    public void onPauseButtonPress()
    {
        onPause();
    }

    public void onOptionsButtonPress()
    {
        //unload pause menu
        //load options menu
        pauseMenu.SetActive(false);
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

    public void onOptionsBackButtonPress()
    {
        //unload options menu
        //load pause menu
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void onMenuButtonPress()
    {
        //load a mini 'confirm' menu
        confirmMenu.SetActive(true);
    }

    public void onConfirmMenuPress()
    {
        // sceneManager.load main menu
        Time.timeScale=1;
        SceneManager.LoadScene("MainMenu");
    }

    public void onCancelMenuPress()
    {
        //unload confirm menu
        confirmMenu.SetActive(false);
    }

    public bool getIsPaused()
    {
        return isPaused;
    }

    void SFXSliderChange()
    {
        optionsHolder.setSFXMultiplier(SFXSlider.value);
    }

    void MusicSliderChange()
    {
        optionsHolder.setMusicMultiplier(MusicSlider.value);
    }

    void SetSliderPositions()
    {
        SFXSlider.value=optionsHolder.getSFXMultiplier();
        MusicSlider.value=optionsHolder.getMusicMultiplier();
    }

    public void ApplyOptionsButton()
    {
        SFXSliderChange();
        MusicSliderChange();
    }
}

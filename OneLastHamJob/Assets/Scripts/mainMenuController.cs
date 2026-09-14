using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour
{
    public void onStartButtonPress()
    {
        SceneManager.LoadScene("Level");
    }
}

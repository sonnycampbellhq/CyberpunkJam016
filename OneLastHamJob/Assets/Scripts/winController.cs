using UnityEngine;
using UnityEngine.SceneManagement;

public class winController : MonoBehaviour
{
    public void win()
    {
        SceneManager.LoadScene("WinScene");
    }
}

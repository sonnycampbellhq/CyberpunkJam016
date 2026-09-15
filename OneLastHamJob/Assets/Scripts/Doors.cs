using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool DoorsOpen = false;

    void Update()
    {
        if (DoorsOpen)
        {
            gameObject.SetActive(false);
        }
    }
}

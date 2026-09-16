using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    Vector2 playerPos;
    [SerializeField]
    Vector4 cameraBounds;
    [SerializeField]
    GameObject levelPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void DoorInteract()
    {
        //set new player position
        //set new camera bounds
        //load in new level elements
        //unload old level elements
    }

    public Vector2 getNewPlayerPosition()
    {
        return playerPos;
    }

    public Vector4 getNewCameraBounds()
    {
        Debug.Log("Instead put the camera bounds in an accessible script within each sub level and access it on creation to reduce manual camera bounds writing");
        Debug.Log("Also with playerPos??");
        return cameraBounds;
    }

    public GameObject getNewLevelPrefab()
    {
        return levelPrefab;
    }


}

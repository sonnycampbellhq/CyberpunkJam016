using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;

    [SerializeField]
    GameObject player;
    PlayerController playerMovementScript;
    float offset = 2.5f;
    float offsetTarget=0;

    float referenceAspect = (float)1920/1080;
    float referenceSize = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

        playerMovementScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float playerDirection = playerMovementScript.getDirection();
        if (playerDirection != 0)
        {
            offsetTarget = player.transform.position.x+playerDirection*offset;
        }
        float xPos = Mathf.Lerp(transform.position.x, offsetTarget, 1f-Mathf.Exp(-5f*Time.deltaTime));
        transform.position = new Vector3(xPos, 2.5f, -10);
    }

    void setResolution()
    {
        Debug.Log("I dont think this works");
        cam = GetComponent<Camera>();
        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect <= referenceAspect)
        {
            cam.orthographicSize = referenceSize * referenceAspect/currentAspect;
        }
    }
}

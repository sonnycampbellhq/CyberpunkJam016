using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    Camera cam;

    [SerializeField]
    GameObject player;
    PlayerController playerMovementScript;

    InputAction look;

    float xOffset = 2.5f;
    float offsetTarget=0;
    float yOffset = 3;
    float yPos=0;

    float referenceAspect = (float)1920/1080;
    float referenceSize = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovementScript = player.GetComponent<PlayerController>();

        look = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float playerDirection = playerMovementScript.getDirection();
        if (playerDirection != 0)
        {
            offsetTarget = player.transform.position.x+playerDirection*xOffset;
        }
        lookVerticalCheck();
        float xPos = Mathf.Lerp(transform.position.x, offsetTarget, 1f-Mathf.Exp(-5f*Time.deltaTime));
        transform.position = new Vector3(xPos, yPos, -10);
    }

    void lookVerticalCheck()
    {
        //move this into lateupdate?
        Vector2 lookDir = look.ReadValue<Vector2>();
        yPos = Mathf.Lerp(transform.position.y, player.transform.position.y+1+yOffset*lookDir.y, 1f-Mathf.Exp(-5f*Time.deltaTime));
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

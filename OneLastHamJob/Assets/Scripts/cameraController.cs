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
    float xOffsetTarget=0;
    float yOffset = 3;
    float yPos=0;

    float xMax;
    float yMax;

    float xMin;
    float yMin;

    Vector4 cameraRanges;

    float referenceAspect = (float)1920/1080;
    float referenceSize = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovementScript = player.GetComponent<PlayerController>();
        cameraRanges = playerMovementScript.GetCameraRanges();

        /*
        xMin=cameraRanges.x;
        xMax=cameraRanges.y;
        yMin=cameraRanges.z;
        yMax=cameraRanges.w;
        */

        xMin=10000;
        xMax=10000;
        yMin=10000;
        yMax=10000;

        look = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float playerDirection = playerMovementScript.getDirection();
        if (playerDirection != 0)
        {
            xOffsetTarget = player.transform.position.x+playerDirection*xOffset;
        }

        Vector2 lookDir = look.ReadValue<Vector2>();
        yPos = Mathf.Lerp(transform.position.y, player.transform.position.y+1+yOffset*lookDir.y, 1f-Mathf.Exp(-5f*Time.deltaTime));

        if(yPos > yMax)
        {
            yPos=yMax;
        }
        else if (yPos < yMin)
        {
            yPos=yMin;
        }
        

        if (xOffsetTarget > xMax)
        {
            xOffsetTarget=xMax;
        }
        else if(xOffsetTarget < xMin)
        {
            xOffsetTarget=xMin;
        }

        float xPos = Mathf.Lerp(transform.position.x, xOffsetTarget, 1f-Mathf.Exp(-5f*Time.deltaTime));
        transform.position = new Vector3(xPos, yPos, -10);
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

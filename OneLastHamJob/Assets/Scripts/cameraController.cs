using Unity.VisualScripting;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    playerMovement playerMovementScript;
    float offset = 2.5f;
    float offsetTarget=0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovementScript = player.GetComponent<playerMovement>();
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
        transform.position = new Vector3(xPos, 3, -10);
    }
}

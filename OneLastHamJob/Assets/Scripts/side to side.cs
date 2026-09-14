using UnityEngine;

public class SidetoSide : MonoBehaviour
{
    public float bobWidth = 0.5f;    
    public float bobSpeed = 1f;       

    private Vector3 startPosition;
    private float randomPhase;       
    void Start()
    {
        
        startPosition = transform.position;

        
        randomPhase = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
    
        float newX = Mathf.Sin(Time.time * bobSpeed + randomPhase) * bobWidth;

        transform.position = new Vector3(startPosition.x + newX, startPosition.y, startPosition.z);
    }
}

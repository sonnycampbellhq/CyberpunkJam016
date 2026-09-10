using UnityEngine;

public class HealthBoxController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int healthInteract()
    {
        Destroy(gameObject);
        return 1;
    }
}

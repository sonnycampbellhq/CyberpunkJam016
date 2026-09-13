using UnityEngine;

public class HealthBoxController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    BillboardController interactSign;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactSign = GetComponentInChildren<BillboardController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int healthInteract()
    {
        interactSign.preDestroy();
        Destroy(gameObject);
        return 1;
    }
}

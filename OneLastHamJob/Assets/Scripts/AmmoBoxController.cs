using UnityEngine;

public class AmmoBoxController : MonoBehaviour
{
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

    public int ammoInteract()
    {
        interactSign.preDestroy();
        Destroy(gameObject);
        return 6;
    }
}

using UnityEngine;

public class AmmoBoxController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int interact()
    {
        Destroy(gameObject);
        return 6;
    }
}

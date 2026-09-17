using Unity.VisualScripting;
using UnityEngine;

public class BillboardController : MonoBehaviour
{
    bool inRange = false;
    float scale=0;
    float scaleRate = 5;
    float scaleMax=5;

    float normalisedXScale;
    float normalisedYScale;

    [SerializeField]
    float xOffset;
    
    Vector3 defaultPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        normalisedXScale = 1/transform.parent.localScale.x;
        normalisedYScale = 1/transform.parent.localScale.y;
        defaultPosition = transform.position;

        //xOffset=xOffset*normalisedXScale;
    }

    // Update is called once per frame
    void Update()
    {
        updateScale();
        if (inRange)
        {
            updatePosition();
        }
    }

    void updateScale()
    {
        if (inRange&&scale<=4.9)
        {
            scale=scale+scaleRate*(scaleMax-scale)*Time.deltaTime;
        }
        else if (inRange&&scale > 4.9)
        {
            scale=scaleMax;
        }
        else if(scale>0.1)
        {
            scale=scale-5*scaleRate*Time.deltaTime/scale;
        }
        else
        {
            scale=0;
        }
        transform.localScale=new Vector3(scale*normalisedXScale, scale*normalisedYScale, 0);
    }

    public void preDestroy()
    {
        transform.parent=null;
        inRange = false;
        Destroy(gameObject, 1);
    }

    void updatePosition()
    {
        Vector3 tempPosition = defaultPosition;
        tempPosition.y = defaultPosition.y*normalisedYScale+Mathf.Cos(Time.time*2)/3;
        tempPosition.x=xOffset+transform.parent.position.x;
        transform.position = tempPosition;
    }

    public void setInRange(bool inRangeIn)
    {
        inRange = inRangeIn;
    }
}

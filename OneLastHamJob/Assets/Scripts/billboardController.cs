using UnityEngine;

public class billboardController : MonoBehaviour
{
    bool inRange = false;
    float scale=0;
    float scaleRate = 5;
    float scaleMax=5;

    Vector3 defaultPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        updateScale();
        if (inRange)
        {
            updateHeight();
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
        //also deal with when parent is removed, set this as no oarent, continue running script and destroy in tmax secs
        //also experiment with rotation]

        transform.localScale=new Vector3(scale, scale, 0);
    }

    void updateHeight()
    {
        Vector3 tempPosition = defaultPosition;
        tempPosition.y = defaultPosition.y+Mathf.Cos(Time.time*2)/3;
        transform.position = tempPosition;
    }

    public void setInRange(bool inRangeIn)
    {
        inRange = inRangeIn;
    }
}

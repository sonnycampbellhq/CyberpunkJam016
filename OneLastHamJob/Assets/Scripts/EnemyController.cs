using Unity.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    int direction;
    [SerializeField]
    float moveSpeed = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        direction = (int)Mathf.Sign(player.transform.position.x - transform.position.x);
        transform.Translate(direction*moveSpeed*Time.deltaTime*getVariableMoveSpeed1(),0,0);
    }

    float getVariableMoveSpeed1()
    {
        float x = Time.time;
        return Mathf.Sin(5*x)+Mathf.Abs(Mathf.Sin(5*x));
    }

    float getVariableMoveSpeed2()
    {
        float x = Time.time;
        return Mathf.Sign(Mathf.Sin(2*x)*Mathf.Abs(Mathf.Sin(2*x))+1)/2;
    }

    public void die()
    {
        Debug.Log("BLEUGHGGHH");
        Destroy(gameObject);
    }
}

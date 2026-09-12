using Unity.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    int direction;
    [SerializeField]
    float moveSpeed = 2;
    GameObject bloodParticleSystem;
    int health=3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bloodParticleSystem = transform.GetChild(0).gameObject;
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

    public void damage(float damageAngle)
    {
        bloodParticleSystem.transform.rotation=Quaternion.Euler(0, 0, damageAngle-90);
        bloodParticleSystem.GetComponent<ParticleSystem>().Play();
        health--;
        if (health <= 0)
        {
            // on death, spawn in a copy of the mesh, which is ragdolled, and doesn't have the enemy tag
            // have a darker colour so it's clearly dead, and emit a final blood particle
            Debug.Log("Fix the death");
            Destroy(gameObject); 
        }
    }
}

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Camera camera;
    InputAction movement;
    InputAction shoot;
    InputAction interact;

    List<GameObject> interactableItems;

    [SerializeField]
    float moveSpeed=5;
    float direction;

    [SerializeField]
    int startAmmo;
    int ammo;
    Vector3 playerShootOffset = new Vector3(0, 0.5f, 0);

    [SerializeField]
    int startHealth;
    int health;

    [SerializeField]
    float invincibilityDuration;
    float lastHit = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = InputSystem.actions.FindAction("Move");
        shoot = InputSystem.actions.FindAction("Attack");
        interact = InputSystem.actions.FindAction("Interact");

        ammo = startAmmo;
        health = startHealth;
        interactableItems = new List<GameObject>();

        Debug.Log("There is an issue where if you stay inside an enemy, you will not take damage after your invincibility is up. I will fix this at some point and make you flash when invincible or something");
        Debug.Log("Found another issue where clicking on an enemy doesn't kill it, think it's because ray is shot from player centre so aiming is unnatural");
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        mouseDetection();
        interactCheck();
    }

    void playerMovement()
    {
        direction = Mathf.Ceil(movement.ReadValue<Vector2>().x);
        transform.position += new Vector3(direction*Time.deltaTime*moveSpeed, 0, 0);
    }

    void interactCheck()
    {
        if (interact.triggered&&interactableItems.Count>0)
        {
            GameObject itemTemp = interactableItems[0];

            //appropriate if statement here?!
            interactableItems.Remove(itemTemp);
            if (itemTemp.tag == "Ammo")
            {
                ammo+=itemTemp.GetComponent<AmmoBoxController>().ammoInteract();//change to accept all consumable types
                Debug.Log("Gained 6 ammo:" + ammo);
            }
            else if(itemTemp.tag == "Health")
            {
                health+=itemTemp.GetComponent<HealthBoxController>().healthInteract();
                Debug.Log("gained 1 health: "+health);
            }
        }
    }

    void mouseDetection()
    {
        Debug.Log("Ammo count: "+ammo);
        if (shoot.triggered)
        {
            if (ammo > 0)
            {
                ammo--;
            
                Ray clickRayZPlane = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
                float zPlaneVectorDistance = clickRayZPlane.origin.z/clickRayZPlane.direction.z;
                Vector3 clickWorldSpaceVector = clickRayZPlane.origin - zPlaneVectorDistance*clickRayZPlane.direction;
                Vector3 shootVector = clickWorldSpaceVector - (transform.position+playerShootOffset);

                RaycastHit[] hits = new RaycastHit[5];
            
                if (Physics.RaycastNonAlloc(transform.position+playerShootOffset, shootVector.normalized, hits)!=0)
                {
                    bool enemyHit = false;
                    GameObject objectHit = hits[0].collider.gameObject;
                    for(int i=0; i<5; i++)
                    {
                        if (objectHit.tag=="Enemy")
                        {
                            objectHit.GetComponent<EnemyController>().die();
                            enemyHit=true;
                        }
                    }

                    if(!enemyHit)Debug.Log("didn't hit a single enemy");
                }
                else
                {
                    Debug.Log("missed me bitch");    
                }
            }
            else
            {
                Debug.Log("Out of ammo");
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject colliderGO = collider.gameObject;
        if (colliderGO.tag == "Enemy")
        {
            //take damage, start invincibility timer
            if (!invincibilityCheck())
            {
                lastHit=Time.time;
                health--;
                Debug.Log("health: "+health);
            }
        }
        else
        {
            interactableItems.Add(collider.gameObject);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        GameObject colliderGO = collider.gameObject;
        if (colliderGO.tag != "Enemy")
        {
            interactableItems.Remove(collider.gameObject);
        }
    }

    bool invincibilityCheck()
    {
        return (lastHit+invincibilityDuration>Time.time);
    }

    public float getDirection()
    {
        return direction;
    }
}

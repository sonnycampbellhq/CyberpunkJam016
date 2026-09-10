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
    int maxAmmo;
    int ammo;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = InputSystem.actions.FindAction("Move");
        shoot = InputSystem.actions.FindAction("Attack");
        interact = InputSystem.actions.FindAction("Interact");

        ammo = maxAmmo;
        interactableItems = new List<GameObject>();
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
            ammo+=itemTemp.GetComponent<AmmoBoxController>().interact();//change to accept all consumable types
            Debug.Log("Gained 6 ammo");
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
                Vector3 shootVector = clickWorldSpaceVector - transform.position;

                RaycastHit[] hits = new RaycastHit[1];
            
                if (Physics.RaycastNonAlloc(transform.position, shootVector.normalized, hits)!=0)
                {
                    GameObject objectHit = hits[0].collider.gameObject;
                    if (objectHit.tag=="Enemy")
                    {
                        objectHit.GetComponent<EnemyController>().die();
                    }
                    else
                    {
                        Debug.Log("hit a non-enemy");
                        //if doesn't hit an enemy, do some particle stuff??
                    }
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
        interactableItems.Add(collider.gameObject);
    }

    void OnTriggerExit(Collider collider)
    {
        interactableItems.Remove(collider.gameObject);
    }

  public float getDirection()
    {
        return direction;
    }
}

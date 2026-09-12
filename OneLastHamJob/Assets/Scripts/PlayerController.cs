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
    RenderTexture resolutionTarget;
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
    Vector3 playerShootOffset = new Vector3(0, 0, 0);

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

        resolutionTarget = camera.targetTexture;
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
            }
            else if(itemTemp.tag == "Health")
            {
                health+=itemTemp.GetComponent<HealthBoxController>().healthInteract();
            }
        }
    }


    [SerializeField]
    RectTransform rectTransform;

    void mouseDetection()
    {
        if (shoot.triggered)
        {
            if (ammo > 0)
            {
                ammo--;

                //very annoying vector stuff to get the vector from the player to the mouse on the z=0 plane
                Vector2 mouse = Mouse.current.position.ReadValue();
                Vector2 renderTexturePosition = new Vector2(mouse.x/Screen.width*resolutionTarget.width, mouse.y/Screen.height*resolutionTarget.height);
                Ray clickRayZPlane = camera.ScreenPointToRay(renderTexturePosition);
                float zPlaneVectorDistance = clickRayZPlane.origin.z/clickRayZPlane.direction.z;
                Vector3 clickWorldSpaceVector = clickRayZPlane.origin - zPlaneVectorDistance*clickRayZPlane.direction;
                Vector3 shootVector = clickWorldSpaceVector - (transform.position+playerShootOffset);

                Debug.Log(shootVector);

                RaycastHit[] hits = new RaycastHit[5];
                int hitCount = Physics.RaycastNonAlloc(transform.position+playerShootOffset, shootVector.normalized, hits);
                for(int i=0; i<hitCount; i++)
                {
                    GameObject objectTemp = hits[i].collider.gameObject;
                    if (objectTemp.tag=="Enemy")
                    {
                        objectTemp.GetComponent<EnemyController>().die();
                    }
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
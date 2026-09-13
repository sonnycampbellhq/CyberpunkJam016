using System;
using System.Collections.Generic;
using Unity.VectorGraphics;
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
    float hitInvincibilityDuration;
    float lastHit=-10;
    bool hitInvincible = false;

    Color playerDefaultColour;

    GameObject bloodParticleSystem;
    
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

        playerDefaultColour = gameObject.GetComponent<MeshRenderer>().material.color;

        bloodParticleSystem = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        mouseDetection();
        interactCheck();
        hitInvincible = hitInvincibilityCheck();
        if (hitInvincible)
        {
            updateInvincibilityFlash();
        }
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

                RaycastHit[] hits = new RaycastHit[5];
                int hitCount = Physics.RaycastNonAlloc(transform.position+playerShootOffset, shootVector.normalized, hits);
                for(int i=0; i<hitCount; i++)
                {
                    GameObject objectTemp = hits[i].collider.gameObject;
                    if (objectTemp.tag=="Enemy")
                    {
                        objectTemp.GetComponent<EnemyController>().damage(vectorToAngle(shootVector));
                    }
                }
            }
            else
            {
                Debug.Log("Out of ammo");
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            takeDamageCheck(collider.gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject colliderGO = collider.gameObject;
        if (colliderGO.tag != "Enemy")
        {
            interactableItems.Add(collider.gameObject);
            collider.gameObject.GetComponentInChildren<billboardController>().setInRange(true);
        }
    }

  void OnTriggerExit(Collider collider)
    {
        GameObject colliderGO = collider.gameObject;
        if (colliderGO.tag != "Enemy")
        {
            interactableItems.Remove(collider.gameObject);
            collider.gameObject.GetComponentInChildren<billboardController>().setInRange(false);
        }
    }

    void takeDamageCheck(GameObject enemyGO)
    {
        if (!hitInvincible)
            {
                lastHit=Time.time;
                health--;
                Debug.Log(health);

                onHitParticles(enemyGO.transform.position);
            }
    }

    void onHitParticles(Vector3 enemyPosition)
    {
        //get angle to enemy that damaged player

        //angle=thatAngle+180
        //set particlerotation.z to that val
        float angle = vectorToAngle(enemyPosition-transform.position)+90;
        Debug.Log(angle);
        bloodParticleSystem.transform.rotation=Quaternion.Euler(0, 0, angle);
        bloodParticleSystem.GetComponent<ParticleSystem>().Play();
    }

    float vectorToAngle(Vector3 vectorIn)
    {
        return Mathf.Atan2(vectorIn.y, vectorIn.x)*Mathf.Rad2Deg;
    }

    bool hitInvincibilityCheck()
    {
        return (lastHit+hitInvincibilityDuration>Time.time);
    }

    void updateInvincibilityFlash()
    {
        Color tempColour = gameObject.GetComponent<MeshRenderer>().material.color;
        tempColour.a = Mathf.Cos((Time.time-lastHit)*Mathf.PI*5)/2+0.5f;
        gameObject.GetComponent<MeshRenderer>().material.color = tempColour;
        Debug.Log("MAKE THIS FLASH TRANSPARENT and have a shadow");
    }

    public float getDirection()
    {
        return direction;
    }
}
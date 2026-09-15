using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    Camera camera;
    [SerializeField]
    GameMenuController gameMenuController;
    RenderTexture resolutionTarget;
    InputAction movement;
    InputAction shoot;
    InputAction interact;
    InputAction roll;
    InputAction jump;

    List<GameObject> interactableItems;

    [SerializeField]
    float moveSpeed=5;
    float direction;

    [SerializeField]
    int startAmmo;
    int ammo;
    Vector3 playerShootOffset = new Vector3(0, 0, 0);
    Vector3 shootVector;

    [SerializeField]
    int startHealth;
    int health;

    [SerializeField]
    float hitInvincibilityDuration;
    float lastHit=-10;
    bool hitInvincible = false;

    [SerializeField]
    float rollDuration;
    [SerializeField]
    float rollSpeed;
    [SerializeField]
    float rollCooldown;
    float lastRoll=-10;
    float rollDirection;

    bool isRolling;
    bool canRoll;

    bool isGrounded = true;
    float jumpForce = 10;
    bool isJumping = false;

    GameObject bloodParticleSystem;
    GameObject bleedParticleSystem;
    GameObject gunshotParticleSystem;

    [SerializeField]
    Texture2D reticleTexture;

    Transform checkpoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=gameObject.GetComponent<Rigidbody>();

        Cursor.SetCursor(reticleTexture, new Vector2(16,16), CursorMode.Auto);

        movement = InputSystem.actions.FindAction("Move");
        shoot = InputSystem.actions.FindAction("Attack");
        interact = InputSystem.actions.FindAction("Interact");
        roll = InputSystem.actions.FindAction("Roll");
        jump = InputSystem.actions.FindAction("Jump");

        ammo = startAmmo;
        health = startHealth;
        interactableItems = new List<GameObject>();

        resolutionTarget = camera.targetTexture;

        bloodParticleSystem = transform.GetChild(1).gameObject;
        bleedParticleSystem = transform.GetChild(2).gameObject;
        gunshotParticleSystem = transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameMenuController.getIsPaused())
        {
            mouseDetection();
            interactCheck();
            hitInvincible = hitInvincibilityCheck();
            if (hitInvincible)
            {
                updateInvincibilityFlash();
            }
            jumpCheck();
            rollCheck();
        }
    }

    void FixedUpdate()
    {
        playerMovement();
        if (isJumping)
        {
            addJumpForce();
        }
    }

    void playerMovement()
    {
        if (!isRolling)
        {
            direction = Mathf.Ceil(movement.ReadValue<Vector2>().x);
            transform.position += new Vector3(direction*Time.deltaTime*moveSpeed, 0, 0);
        }
    }

    void jumpCheck()
    {
        if (isGrounded&&jump.triggered)
        {
            isJumping = true;
        }
    }

    void addJumpForce()
    {
        rb.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
        isGrounded=false;
        isJumping=false;
    }

    void rollCheck()
    {
        updateIsRolling();
        updateCanRoll();
        if (!isRolling)
        {
            rollDirection=0;
        }

        if (canRoll)
        {
            if (roll.triggered&&direction!=0)
            {
                lastRoll = Time.time;
                rollDirection = direction;
                Debug.Log("roll animation WHOA");
            }
        }
        else if (isRolling)
        {
            //roll ongoing
            transform.position += new Vector3(rollDirection*Time.deltaTime*rollSpeed, 0, 0);
        }
    }

    void updateIsRolling()
    {
        isRolling = lastRoll+rollDuration > Time.time;
    }

    void updateCanRoll()
    {
        canRoll = (lastRoll+rollCooldown < Time.time)&&isGrounded;
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
                bleedParticleSystem.GetComponent<ParticleSystem>().Stop();
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

                //gets angle from player to mouse
                Vector2 mouse = Mouse.current.position.ReadValue();
                Vector2 renderTexturePosition = new Vector2(mouse.x/Screen.width*resolutionTarget.width, mouse.y/Screen.height*resolutionTarget.height);
                Ray clickRayZPlane = camera.ScreenPointToRay(renderTexturePosition);
                float zPlaneVectorDistance = clickRayZPlane.origin.z/clickRayZPlane.direction.z;
                Vector3 clickWorldSpaceVector = clickRayZPlane.origin - zPlaneVectorDistance*clickRayZPlane.direction;
                shootVector = clickWorldSpaceVector - (transform.position+playerShootOffset);

                //create gunshot particles
                gunshotParticleSystem.transform.rotation=Quaternion.Euler(0, 0, vectorToAngle(shootVector)+90);
                gunshotParticleSystem.GetComponent<ParticleSystem>().Play();

                //gets and sorts hits by distance
                RaycastHit[] hits = new RaycastHit[5];
                int hitCount = Physics.RaycastNonAlloc(transform.position+playerShootOffset, shootVector.normalized, hits, maxDistance: 10);
                float[] hitDistances = new float[hitCount];
                for(int k = 0; k<hitCount; k++)
                {
                    hitDistances[k]=hits[k].distance;
                }
                Array.Sort(hitDistances, hits);

                //checks what is hit, stops checking if hits wall
                for(int i=0; i<hitCount; i++)
                {
                    GameObject objectTemp = hits[i].transform.gameObject;
                    if (objectTemp.tag=="Enemy")
                    {
                        objectTemp.GetComponent<EnemyController>().damage(vectorToAngle(shootVector));
                    }
                    else if (objectTemp.tag == "Wall")
                    {
                        i=hitCount;
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
            collider.gameObject.GetComponentInChildren<BillboardController>().setInRange(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        GameObject colliderGO = collider.gameObject;
        if (colliderGO.tag != "Enemy")
        {
            interactableItems.Remove(collider.gameObject);
            collider.gameObject.GetComponentInChildren<BillboardController>().setInRange(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        for (int i=0;i<collision.contactCount;i++)
        {
            if (collision.contacts[i].normal.y > 0.1)
            {
                isGrounded=true;
                rb.linearVelocity=Vector3.zero;
            }
        }
    }

    void takeDamageCheck(GameObject enemyGO)
    {
        if (!hitInvincible && !isRolling)
        {
            lastHit = Time.time;
            health--;
            Debug.Log(health);

            onHitParticles(enemyGO.transform.position);

            if (health <= 0)
            {
                Respawn();
            }
        }
    }


    void onHitParticles(Vector3 enemyPosition)
    {
        //get angle to enemy that damaged player
        float angle = vectorToAngle(enemyPosition-transform.position)+90;
        bloodParticleSystem.transform.rotation=Quaternion.Euler(0, 0, angle);
        bloodParticleSystem.GetComponent<ParticleSystem>().Play();
        if (health == 1)
        {
            bleedParticleSystem.GetComponent<ParticleSystem>().Play();
        }
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
        return direction+rollDirection;
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        checkpoint = newCheckpoint;
        Debug.Log("Checkpoint set!");
    }

    public void Respawn()
    {
        if (checkpoint != null)
        {
            health = startHealth;
            rb.linearVelocity = Vector3.zero;
            transform.position = checkpoint.position;
            transform.rotation = checkpoint.rotation;
            bleedParticleSystem.GetComponent<ParticleSystem>().Stop();
        }
    }

}

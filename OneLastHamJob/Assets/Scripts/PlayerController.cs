using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Camera camera;
    InputAction movement;
    InputAction shoot;
    [SerializeField]
    float moveSpeed=5;
    float direction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = InputSystem.actions.FindAction("Move");
        shoot = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        mouseDetection();
    }

    void playerMovement()
    {
        direction = Mathf.Ceil(movement.ReadValue<Vector2>().x);
        transform.position += new Vector3(direction*Time.deltaTime*moveSpeed, 0, 0);
    }

    void mouseDetection()
    {
        if (shoot.triggered)
        {
            Ray clickRayZPlane = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            float zPlaneVectorDistance = clickRayZPlane.origin.z/clickRayZPlane.direction.z;
            Vector3 clickWorldSpaceVector = clickRayZPlane.origin - zPlaneVectorDistance*clickRayZPlane.direction;
            Vector3 shootVector = clickWorldSpaceVector - transform.position;

            RaycastHit[] hits = new RaycastHit[10];
            Debug.Log(Physics.RaycastNonAlloc(transform.position, shootVector.normalized, hits));

            Debug.Log(shootVector+":::"+hits[0].collider.gameObject.name);



            Debug.DrawLine(transform.position, shootVector, Color.red, 5);



        }
    }

    public float getDirection()
    {
        return direction;
    }
}

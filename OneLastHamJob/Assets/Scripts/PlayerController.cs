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
            Debug.DrawLine(transform.position, camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Color.black, 1);
            Debug.Log("yuh");
        }
    }

    public float getDirection()
    {
        return direction;
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    InputAction movement;
    InputAction shoot;
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
        direction = movement.ReadValue<Vector2>().x;
        Vector2 moveAmount = movement.ReadValue<Vector2>()*Time.deltaTime*5;
        transform.position+=(Vector3)moveAmount;
        mouseDetection();
    }

    void mouseDetection()
    {
        if (shoot.triggered)
        {
            Debug.DrawLine(transform.position, Input.mousePosition);
            Debug.Log("yuh");
        }
    }

    public float getDirection()
    {
        return direction;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerHandler : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    InputAction moveAction;

    
    // Start is called before the first frame update
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        movementCheck();
    }

    void movementCheck()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        transform.Translate(moveVector*Time.deltaTime*moveSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Player player;
    private MovementByVelocityEvent movementByVelocityEvent;
    private float moveSpeed = 1000.0f;
    private PlayerInput inputActions;
    private InputAction move;
    private Vector2 direction;
    private void Awake()
    {
        // Load components
        player = GetComponent<Player>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();

        // Initialize input actions
        inputActions = new PlayerInput();

    }
    private void OnEnable()
    {
        move = inputActions.Player.Move;
        move.Enable();

    }
    private void OnDisable()
    {
        move.Disable();
    }
    private void Update()
    {
        MovementInput();
    }
    /// <summary>
    /// Player movement input
    /// </summary>
    private void MovementInput()
    {

        // Create a direction vector based on the input
        direction = move.ReadValue<Vector2>();
        // If there is movement either move 
        if (direction != Vector2.zero)
        {
            // trigger movement event
            player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed);

        }

    }
}

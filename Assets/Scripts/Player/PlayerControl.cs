using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Player player;
    private MovementByVelocityEvent movementByVelocityEvent;
    private float moveSpeed = 6.0f;
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
        // If there is movement either move or roll
        if (direction != Vector2.zero)
        {
            if (direction == Vector2.up)
            {
                player.animateEvent.CallAnimateEvent(AimDirection.Up);
            }
            else if (direction == Vector2.down)
            {
                player.animateEvent.CallAnimateEvent(AimDirection.Down);
            }
            else if (direction == Vector2.left)
            {
                player.animateEvent.CallAnimateEvent(AimDirection.Left);
            }
            else if (direction == Vector2.right)
            {
                player.animateEvent.CallAnimateEvent(AimDirection.Right);
            }
            else if (direction == new Vector2(-0.71f, -0.71f))
            {
                player.animateEvent.CallAnimateEvent(AimDirection.DownLeft);
            }
            else if (direction == new Vector2(0.71f, -0.71f))
            {
                player.animateEvent.CallAnimateEvent(AimDirection.DownRight);
            }
            else if (direction == new Vector2(-0.71f, 0.71f))
            {
                player.animateEvent.CallAnimateEvent(AimDirection.UpLeft);
            }
            else if (direction == new Vector2(0.71f, 0.71f))
            {
                player.animateEvent.CallAnimateEvent(AimDirection.UpRight);
            }
            // trigger movement event
            player.movementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed);

        }
        // else trigger idle event
        else
        {
            player.idleEvent.CallIdleEvent();
        }

    }
}

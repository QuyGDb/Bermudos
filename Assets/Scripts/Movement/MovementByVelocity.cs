using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[DisallowMultipleComponent]
public class MovementByVelocity : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private MovementByVelocityEvent movementByVelocityEvent;
    private DestroyedEvent destroyedEvent;
    private void Awake()
    {
        // Load components
        rigidBody2D = GetComponent<Rigidbody2D>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
    }

    private void OnEnable()
    {
        // Subscribe to movement event
        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        destroyedEvent.OnDestroyed += DestroyedEvent_OnDestroyed;
    }

    private void OnDisable()
    {
        // Unsubscribe from movement event
        movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        destroyedEvent.OnDestroyed -= DestroyedEvent_OnDestroyed;
    }



    private void DestroyedEvent_OnDestroyed(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
    {
        if (destroyedEventArgs.playerDied)
        {
            movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        }
    }
    // On movement event
    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityArgs movementByVelocityArgs)
    {
        MoveRigidBody(movementByVelocityArgs.moveDirection, movementByVelocityArgs.moveSpeed);
    }

    /// <summary>
    /// Move the rigidbody component
    /// </summary>
    private void MoveRigidBody(Vector2 moveDirection, float moveSpeed)
    {
        // ensure the rb collision detection is set to continuous
        rigidBody2D.velocity = moveDirection * moveSpeed;


        // rigidBody2D.MovePosition(rigidBody2D.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}

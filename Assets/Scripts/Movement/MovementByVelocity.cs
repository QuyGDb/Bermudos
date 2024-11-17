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
    private DieEvent dieEvent;
    private void Awake()
    {
        // Load components
        rigidBody2D = GetComponent<Rigidbody2D>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        dieEvent = GetComponent<DieEvent>();
    }

    private void OnEnable()
    {
        // Subscribe to movement event
        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        destroyedEvent.OnDestroyed += DestroyedEvent_OnDestroyed;
        dieEvent.OnDie += DieEvent_OnDie;
    }

    private void OnDisable()
    {
        // Unsubscribe from movement event
        movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        destroyedEvent.OnDestroyed -= DestroyedEvent_OnDestroyed;
        dieEvent.OnDie -= DieEvent_OnDie;
    }



    private void DestroyedEvent_OnDestroyed(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
    {
        if (destroyedEventArgs.playerDied)
        {
            movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        }
    }
    private void DieEvent_OnDie(DieEvent dieEvent)
    {
        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
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
    }
}

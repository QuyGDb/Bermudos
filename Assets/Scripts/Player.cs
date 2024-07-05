using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public MovementByVelocityEvent movementByVelocityEvent;
    private MovementByVelocity movementByVelocity;

    private void Awake()
    {
        // Load components
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        movementByVelocity = GetComponent<MovementByVelocity>();
    }
}

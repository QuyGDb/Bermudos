using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public MovementByVelocityEvent movementByVelocityEvent;
    public IdleEvent idleEvent;
    public AnimateEvent animateEvent;
    public Animator animator;
    private void Awake()
    {
        // Load components
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        idleEvent = GetComponent<IdleEvent>();
        animateEvent = GetComponent<AnimateEvent>();
        animator = GetComponent<Animator>();
    }
}

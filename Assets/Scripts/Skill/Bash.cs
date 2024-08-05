using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour
{
    private Rigidbody rigidbody2d;
    private Collider2D collider2d;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        collider2d = Physics2D.OverlapCircle(transform.position, 0.5f);
    }
}

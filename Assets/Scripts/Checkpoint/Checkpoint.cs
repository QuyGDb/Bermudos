using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LayerMask playerLayer;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        Debug.Log("Player Layer: " + playerLayer.value);
        playerLayer = LayerMask.GetMask("Player");
        Debug.Log("Player Layer: " + playerLayer.value);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((playerLayer.value & 1 << collision.gameObject.layer) > 0)
        {
            Debug.Log("Player has reached the checkpoint");
        }
    }
}

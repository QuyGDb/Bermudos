using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float timeToDestroy = 10f;
    private LayerMask playerLayerMark;

    private void Start()
    {
        playerLayerMark = LayerMask.GetMask("Player");
    }
    private void Update()
    {
        if (timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timeToDestroy -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((playerLayerMark.value & 1 << collision.gameObject.layer) > 0)
        {

            Destroy(gameObject);
        }
    }
}

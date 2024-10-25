using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private bool isColliding = false;
    private int damageBeam = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
            return;
        collision.GetComponent<ReceiveDamage>()?.TakeDamage(damageBeam);
        collision.GetComponent<PlayerEffect>()?.DamagePushEfect();
        collision.GetComponent<PlayerEffect>()?.CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
        isColliding = true;
    }
}

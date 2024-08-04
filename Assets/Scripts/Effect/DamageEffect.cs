using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEfect : MonoBehaviour
{
    public AnimationCurve damageFlashCurve;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private Coroutine damagePushCoroutine;
    private DamagePushEfectEvent damagePushEfectEvent;
    public float damageForce;
    public float duration = 0.25f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
        damagePushEfectEvent = GetComponent<DamagePushEfectEvent>();
    }

    private void OnEnable()
    {
        damagePushEfectEvent.OnDamagePushEfect += DamagePushEfectEvent_OnDamagePushEfect;
    }

    private void OnDisable()
    {
        damagePushEfectEvent.OnDamagePushEfect -= DamagePushEfectEvent_OnDamagePushEfect;
    }

    private void DamagePushEfectEvent_OnDamagePushEfect(DamagePushEfectEvent damagePushEfectEvent)
    {
        DamagePushEfect();
    }
    public void CallDamageFlashEffect(Material damageFlash, Material defaultMaterial, SpriteRenderer spriteRenderer)
    {
        StartCoroutine(DamageFlasCoroutine(damageFlash, defaultMaterial, spriteRenderer));
    }

    private IEnumerator DamageFlasCoroutine(Material damageFlash, Material defaultMaterial, SpriteRenderer spriteRenderer)
    {
        spriteRenderer.material = damageFlash;
        Debug.Log(spriteRenderer.material);
        float flashAmount = 0f;

        while (flashAmount < 1)
        {
            flashAmount += Time.deltaTime * 10;
            float lerp = Mathf.Lerp(0, 1, flashAmount);
            spriteRenderer.material.SetFloat("_FlashAmount", damageFlashCurve.Evaluate(lerp));
            yield return null;
        }

        spriteRenderer.material = defaultMaterial;
        yield return null;
    }

    /// <summary>
    /// push enemy that get attack
    /// </summary>
    public void DamagePushEfect()
    {
        if (damagePushCoroutine != null)
        {
            StopCoroutine(damagePushCoroutine);
        }
        damagePushCoroutine = StartCoroutine(DamagePushCoroutine(rb.position + (damageForce * (rb.position - GameManager.Instance.player.GetPlayerPosition()).normalized)));
    }
    private IEnumerator DamagePushCoroutine(Vector3 targetPosition)
    {
        Debug.Log(enemyMovement);
        Vector3 startPosition = rb.position;
        if (enemyMovement != null)
        {
            enemyMovement.navMeshAgent.isStopped = true;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                // var movementStep = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
                rb.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
                //rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration)));
                //rb.AddForce(targetPosition, ForceMode2D.Impulse); //rb.AddForce
                elapsedTime += Time.fixedDeltaTime;
                yield return waitForFixedUpdate;
            }
            enemyMovement.navMeshAgent.isStopped = false;
        }

    }

}

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
    private Component ammo;
    [ColorUsage(true, true)]
    [SerializeField] private Color flashColor;
    // private DamagePushEfectEvent damagePushEfectEvent;
    public float damageForce;
    public float duration = 0.25f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
        // damagePushEfectEvent = GetComponent<DamagePushEfectEvent>();
    }

    private void OnEnable()
    {
        //damagePushEfectEvent.OnDamagePushEfect += DamagePushEfectEvent_OnDamagePushEfect;
        StaticEventHandler.OnAmmoChanged += StaticEventHandler_OnAmmoChanged;
    }

    private void OnDisable()
    {
        //damagePushEfectEvent.OnDamagePushEfect -= DamagePushEfectEvent_OnDamagePushEfect;
        StaticEventHandler.OnAmmoChanged -= StaticEventHandler_OnAmmoChanged;
    }

    //private void DamagePushEfectEvent_OnDamagePushEfect(DamagePushEfectEvent damagePushEfectEvent)
    //{
    //    DamagePushEfect();
    //}

    private void StaticEventHandler_OnAmmoChanged(OnAmmoChangedEventArgs onAmmoChangedEventArgs)
    {
        ammo = onAmmoChangedEventArgs.ammo;
    }

    public void CallDamageFlashEffect(Material damageFlash, Material defaultMaterial, SpriteRenderer[] spriteRenderers)
    {
        StartCoroutine(DamageFlashCoroutine(damageFlash, defaultMaterial, spriteRenderers));
    }

    private IEnumerator DamageFlashCoroutine(Material damageFlash, Material defaultMaterial, SpriteRenderer[] spriteRenderers)
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.material = damageFlash;
            spriteRenderer.material.SetColor("_FlashColor", flashColor);
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

    }

    /// <summary>
    /// push enemy that get attack
    /// </summary>
    public void DamagePushEfect(bool isPlayer)
    {
        if (damagePushCoroutine != null)
        {
            StopCoroutine(damagePushCoroutine);
        }
        if (isPlayer)
        {
            damagePushCoroutine = StartCoroutine(DamagePushCoroutine(rb.position + (damageForce * (rb.position - (Vector2)ammo.transform.position).normalized), isPlayer));

        }
        else
        {
            damagePushCoroutine = StartCoroutine(DamagePushCoroutine(rb.position + (damageForce * (rb.position - GameManager.Instance.player.GetPlayerPosition()).normalized), isPlayer));
        }

    }
    private IEnumerator DamagePushCoroutine(Vector3 targetPosition, bool isPlayer)
    {
        Vector3 startPosition = rb.position;

        if (!isPlayer)
        {
            enemyMovement.navMeshAgent.isStopped = true;
        }

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
        if (!isPlayer)
        {
            enemyMovement.navMeshAgent.isStopped = false;
        }

    }

}



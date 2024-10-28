using System.Collections;
using UnityEngine;

public class EnemyEffect : Effect
{
    private PoiseEvent poiseEvent;
    private EnemyMovement enemyMovement;
    private Coroutine pushEnemyByAmmoCoroutine;
    // private DamagePushEfectEvent damagePushEfectEvent;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
        poiseEvent = GetComponent<PoiseEvent>();
        // damagePushEfectEvent = GetComponent<DamagePushEfectEvent>();
    }
    private void OnEnable()
    {
        //damagePushEfectEvent.OnDamagePushEfect += DamagePushEfectEvent_OnDamagePushEfect;
        poiseEvent.onPoise += PoiseEvent_onPoiseEvent;
    }

    private void OnDisable()
    {
        //damagePushEfectEvent.OnDamagePushEfect -= DamagePushEfectEvent_OnDamagePushEfect;
        poiseEvent.onPoise -= PoiseEvent_onPoiseEvent;
    }
    private void PoiseEvent_onPoiseEvent(PoiseEvent poiseEvent, PoiseEventArgs poiseEventArgs)
    {
        if (poiseEventArgs.currentPoise <= 0)
        {
            StunEffect(poiseEventArgs.stunTime);
        }
    }
    //private void DamagePushEfectEvent_OnDamagePushEfect(DamagePushEfectEvent damagePushEfectEvent)
    //{
    //    DamagePushEfect();
    //}
    private void Start()
    {
        bloodEffect.Stop();
        stunEffect.Stop();
    }


    public void CallDamageFlashEffect(Material damageFlash, Material defaultMaterial, SpriteRenderer[] spriteRenderers)
    {
        if (gameObject.activeSelf)
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
    public void DamagePushEfect(Vector3 ammo)
    {
        if (gameObject.activeSelf)
            pushEnemyByAmmoCoroutine = StartCoroutine(PushEnemyByAmmo(rb.position + (damageForce * (rb.position - (Vector2)ammo).normalized)));
    }


    private IEnumerator PushEnemyByAmmo(Vector3 targetPosition)
    {
        Vector3 startPosition = rb.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {

            Vector2 lerp = Vector2.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            Vector2 movementSteps = lerp - rb.position;
            if (enemyMovement.navMeshAgent.isOnNavMesh)
            {
                enemyMovement.navMeshAgent.Move(movementSteps);
            }
            elapsedTime += Time.fixedDeltaTime;
            yield return waitForFixedUpdate;
        }
        yield return waitForFixedUpdate;
    }

    public void PushEnemyByWeapon(Vector3 playerPosition)
    {
        Vector3 targetPosition = transform.position + (damageForce * (transform.position - playerPosition).normalized);
        StartCoroutine(PushEnemyByWeaponCoroutine(targetPosition));
    }
    private IEnumerator PushEnemyByWeaponCoroutine(Vector3 targetPosition)
    {
        Vector3 startPosition = rb.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {

            Vector3 lerp = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            Vector3 movementSteps = lerp - transform.position;
            //Debug.Log(movementSteps);
            if (lerp - transform.position != Vector3.zero && enemyMovement.navMeshAgent.isOnNavMesh)
            {
                enemyMovement.navMeshAgent.Move(movementSteps);
            }
            elapsedTime += Time.fixedDeltaTime;
            yield return waitForFixedUpdate;
        }
        yield return waitForFixedUpdate;
    }
    public void BloodEffect()
    {
        bloodEffect.Play();
    }

}



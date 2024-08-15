using System.Collections;
using UnityEngine;

public class DamageEfect : MonoBehaviour
{
    public AnimationCurve damageFlashCurve;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private Coroutine pushPlayerByAmmoCoroutine;
    private Coroutine pushEnemyByAmmoCoroutine;
    private Component ammo;
    [ColorUsage(true, true)]
    [SerializeField] private Color flashColor;
    // private DamagePushEfectEvent damagePushEfectEvent;
    [SerializeField] private float damageForce;
    [SerializeField] float duration = 0.25f;


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
    public void DamagePushEfect(bool isPlayer)
    {
        if (pushPlayerByAmmoCoroutine != null)
        {
            StopCoroutine(pushPlayerByAmmoCoroutine);
        }
        if (isPlayer)
        {
            if (gameObject.activeSelf)
                pushPlayerByAmmoCoroutine = StartCoroutine(PushPlayerByEnemy(rb.position + (damageForce * (rb.position - (Vector2)ammo.transform.position).normalized)));

        }
        else
        {
            if (gameObject.activeSelf)
                pushEnemyByAmmoCoroutine = StartCoroutine(PushEnemyByAmmo(rb.position + (damageForce * (rb.position - (Vector2)ammo.transform.position).normalized)));
        }

    }
    private IEnumerator PushPlayerByEnemy(Vector3 targetPosition)
    {
        Vector3 startPosition = rb.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration)));
            elapsedTime += Time.fixedDeltaTime;
            yield return waitForFixedUpdate;
        }
    }

    private IEnumerator PushEnemyByAmmo(Vector3 targetPosition)
    {
        Vector3 startPosition = rb.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {

            Vector2 lerp = Vector2.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            Vector2 movementSteps = lerp - rb.position;
            if (lerp - rb.position != Vector2.zero)
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
        // Debug.Log("PushEnemyByWeapon");
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
            if (lerp - transform.position != Vector3.zero)
            {
                enemyMovement.navMeshAgent.Move(movementSteps);
            }
            elapsedTime += Time.fixedDeltaTime;
            yield return waitForFixedUpdate;
        }
        yield return waitForFixedUpdate;
    }
}



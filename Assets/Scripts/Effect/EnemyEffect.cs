using System.Collections;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    private PoiseEvent poiseEvent;
    public AnimationCurve damageFlashCurve;
    private Rigidbody2D rb;
    private EnemyMovement enemyMovement;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] private ParticleSystem stunEffect;
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
        poiseEvent = GetComponent<PoiseEvent>();
        // damagePushEfectEvent = GetComponent<DamagePushEfectEvent>();
    }
    private void OnEnable()
    {
        //damagePushEfectEvent.OnDamagePushEfect += DamagePushEfectEvent_OnDamagePushEfect;
        StaticEventHandler.OnAmmoChanged += StaticEventHandler_OnAmmoChanged;
        poiseEvent.onPoise += PoiseEvent_onPoiseEvent;
    }

    private void OnDisable()
    {
        //damagePushEfectEvent.OnDamagePushEfect -= DamagePushEfectEvent_OnDamagePushEfect;
        StaticEventHandler.OnAmmoChanged -= StaticEventHandler_OnAmmoChanged;
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
    public void DamagePushEfect()
    {
        if (gameObject.activeSelf)
            pushEnemyByAmmoCoroutine = StartCoroutine(PushEnemyByAmmo(rb.position + (damageForce * (rb.position - (Vector2)ammo.transform.position).normalized)));
    }


    private IEnumerator PushEnemyByAmmo(Vector3 targetPosition)
    {
        Vector3 startPosition = rb.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {

            Vector2 lerp = Vector2.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            Vector2 movementSteps = lerp - rb.position;
            enemyMovement.navMeshAgent.Move(movementSteps);
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
            if (lerp - transform.position != Vector3.zero)
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
    public void StunEffect(float stunTime)
    {
        ParticleSystem.MainModule mainModule = stunEffect.main;
        mainModule.startLifetimeMultiplier = stunTime;
        stunEffect.Play();

    }
    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(bloodEffect), bloodEffect);
        HelperUtilities.ValidateCheckNullValue(this, nameof(stunEffect), stunEffect);
    }
#endif
    #endregion
}



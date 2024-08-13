using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IFireable
{
    private Vector3 shootDirection;
    private DamageEfect damageEfects;
    private DestroyedEvent destroyedEvent;

    private float speed;
    private float maxSpeed;
    private int damage;
    private LayerMask enemyLayerMask;
    private LayerMask playeLayerMaskr;

    private AnimationCurve trajectoryAnimationCurve;
    private AnimationCurve axisCorrectionAnimationCurve;
    private AnimationCurve ammoSpeedAnimationCurve;
    private Vector2 trajectoryStartPoint;
    private Vector2 target;

    private Vector2 ammoDirection;
    private float trajectoryMaxRelativeHeight;
    private AmmoState ammoState;
    private void Awake()
    {
        damageEfects = GetComponent<DamageEfect>();
        destroyedEvent = GetComponent<DestroyedEvent>();
    }

    private void Start()
    {
        GetLayerMark();

        trajectoryStartPoint = transform.position;
    }
    private void Update()
    {
        switch (ammoState)
        {
            default:
            case AmmoState.Trajectory:
                UpdateAmmoPosition();
                break;
            case AmmoState.Linear:
                MoveAmmoByDirection();
                break;
        }
    }
    private void UpdateAmmoPosition()
    {
        Vector2 trajectoryRange = target - trajectoryStartPoint;
        float distance = trajectoryRange.magnitude;
        if (trajectoryRange.x < 0)
        {
            speed = -speed;
        }
        float nextPositionX = transform.position.x + speed * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;
        float nextPositionYNormalized = trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
        float nextPositionYCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionXNormalized);
        float nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalized * trajectoryRange.y;

        float nextPositionY = trajectoryStartPoint.y + trajectoryMaxRelativeHeight * nextPositionYNormalized + nextPositionYCorrectionAbsolute;
        Vector2 nextPosition = new Vector2(nextPositionX, nextPositionY);
        if (nextPositionXNormalized > 0.99)
        {
            ammoDirection = nextPosition - (Vector2)transform.position;
            ammoState = AmmoState.Linear;
        }
        CalculateNextAmmoMoveSpeed(nextPositionXNormalized);
        transform.position = nextPosition;
    }

    private void MoveAmmoByDirection()
    {
        transform.position += (Vector3)ammoDirection.normalized * maxSpeed * Time.deltaTime;
    }

    private void CalculateNextAmmoMoveSpeed(float nextPositionXNormalized)
    {
        float nextSpeedNormalized = ammoSpeedAnimationCurve.Evaluate(nextPositionXNormalized);
        speed = maxSpeed * nextSpeedNormalized;
    }
    public void InitialiseAmmo(AmmoDetailsSO ammoDetailsSO, Vector3 target, AnimationCurve trajectoryAniamtionCurve, AnimationCurve axisCorrectionAnimationCurve, AnimationCurve ammoSpeedAnimationCurve)
    {
        this.maxSpeed = ammoDetailsSO.maxSpeed;
        this.damage = ammoDetailsSO.damage;
        float xDistanceToTarget = target.x - transform.position.x;
        this.trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget) * ammoDetailsSO.trajectoryMaxHeight;
        SetTargetPosition(target);
        SetAmmoPlayer();
        InitializeAnimationCurves(trajectoryAniamtionCurve, axisCorrectionAnimationCurve, ammoSpeedAnimationCurve);
        gameObject.SetActive(true);
    }

    public void SetShootDirection(Vector3 shootDirection)
    {
        this.shootDirection = shootDirection;
    }

    private void SetAmmoPlayer()
    {
        gameObject.layer = LayerMask.NameToLayer("EnemyAmmo");
    }

    private void SetTargetPosition(Vector3 target)
    {
        this.target = target;
    }

    private void InitializeAnimationCurves(AnimationCurve trajectoryAnimationCurve, AnimationCurve axisCorrectionAnimationCurve, AnimationCurve ammoSpeedAnimationCurve)
    {
        this.trajectoryAnimationCurve = trajectoryAnimationCurve;
        this.axisCorrectionAnimationCurve = axisCorrectionAnimationCurve;
        this.ammoSpeedAnimationCurve = ammoSpeedAnimationCurve;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((playeLayerMaskr.value & 1 << collision.gameObject.layer) > 0)
        {

            Health health = collision.GetComponent<Health>();
            health.TakeDamage(damage);
            StaticEventHandler.CallAmmoChangedEvent(this);
            collision.GetComponent<DamageEfect>().DamagePushEfect(true);
            collision.GetComponent<DamageEfect>().CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
        }

        if ((enemyLayerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            Health health = collision.GetComponent<Health>();
            health.TakeDamage(damage);
            StaticEventHandler.CallAmmoChangedEvent(this);
            collision.GetComponent<DamageEfect>().DamagePushEfect(false);
            collision.GetComponent<DamageEfect>().CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
        }

        AmmoHitEffect();
        DisableAmmo();
    }

    private void AmmoHitEffect()
    {
        AmmoHitEffect ammoHitEffect = PoolManager.Instance.ReuseComponent(GameResources.Instance.ammoHitEffectPrefab, transform.position, Quaternion.identity) as AmmoHitEffect;
        ammoHitEffect.InitialiseAmmoHitEffect();
    }
    private void GetLayerMark()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        playeLayerMaskr = LayerMask.GetMask("Player");
    }
    private void DisableAmmo()
    {
        gameObject.SetActive(false);
    }
}


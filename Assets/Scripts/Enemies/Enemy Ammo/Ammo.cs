using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IFireable
{

    private DamageEfect damageEfects;
    private DestroyedEvent destroyedEvent;
    private AmmoVisual ammoVisual;
    private float speed;
    private float maxSpeed;
    private int damage;
    private LayerMask enemyLayerMask;
    private LayerMask playeLayerMask;

    private AnimationCurve trajectoryAnimationCurve;
    private AnimationCurve axisCorrectionAnimationCurve;
    private AnimationCurve ammoSpeedAnimationCurve;
    private Vector2 trajectoryStartPoint;
    private Vector2 target;
    private Vector2 trajectoryRange;
    private Vector2 ammoMoveDirection;
    private float nextYTrajectoryPosition;
    private float nextXTrajectoryPosition;
    private float trajectoryMaxRelativeHeight;
    private float nextPositionYCorrectionAbsolute;
    private float nextPositionXCorrectionAbsolute;
    public AmmoState ammoState;

    private void Awake()
    {
        damageEfects = GetComponent<DamageEfect>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        ammoVisual = GetComponent<AmmoVisual>();
    }
    private void OnEnable()
    {
        trajectoryStartPoint = transform.position;
    }
    private void Start()
    {
        GetLayerMark();

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
            case AmmoState.Freeze:
                Debug.Log("Freeze");
                break;
        }
    }
    private void UpdateAmmoPosition()
    {
        trajectoryRange = target - trajectoryStartPoint;
        if (Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y))
        {
            if (trajectoryRange.y < 0)
            {
                speed = -speed;
            }
            //ammo will be curve on Y axis
            UpdateAmmoXPosition();
        }
        else
        {
            //ammo will be curve on X axis
            if (trajectoryRange.x < 0)
            {
                speed = -speed;
            }
            UpdateAmmoYPosition();
        }


    }
    private void UpdateAmmoYPosition()
    {
        float nextPositionX = transform.position.x + speed * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;

        float nextPositionYNormalized = trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
        float nextPositionYCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionXNormalized);
        nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalized * trajectoryRange.y;
        nextYTrajectoryPosition = trajectoryMaxRelativeHeight * nextPositionYNormalized;
        if (trajectoryRange.x > 0 && trajectoryRange.y < 0)
        {
            nextYTrajectoryPosition = -nextYTrajectoryPosition;
        }
        if (trajectoryRange.x < 0 && trajectoryRange.y < 0)
        {
            nextYTrajectoryPosition = -nextYTrajectoryPosition;
        }
        float nextPositionY = trajectoryStartPoint.y + nextYTrajectoryPosition + nextPositionYCorrectionAbsolute;
        Vector2 nextPosition = new Vector2(nextPositionX, nextPositionY);
        ammoMoveDirection = nextPosition - (Vector2)transform.position;
        CalculateNextAmmoMoveSpeed(nextPositionXNormalized);
        transform.position = nextPosition;

        if (nextPositionXNormalized > 0.99)
        {
            ammoState = AmmoState.Linear;
        }
    }
    private void UpdateAmmoXPosition()
    {
        float nextPositionY = transform.position.y + speed * Time.deltaTime;
        float nextPositionYNormalized = (nextPositionY - trajectoryStartPoint.y) / trajectoryRange.y;

        float nextPositionXNormalized = trajectoryAnimationCurve.Evaluate(nextPositionYNormalized);
        float nextPositionXCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionYNormalized);

        nextPositionXCorrectionAbsolute = nextPositionXCorrectionNormalized * trajectoryRange.x;

        nextXTrajectoryPosition = trajectoryMaxRelativeHeight * nextPositionXNormalized;
        if (trajectoryRange.x > 0 && trajectoryRange.y > 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }
        if (trajectoryRange.x < 0 && trajectoryRange.y < 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }
        float nextPositionX = trajectoryStartPoint.x + nextXTrajectoryPosition + nextPositionXCorrectionAbsolute;

        Vector2 nextPosition = new Vector2(nextPositionX, nextPositionY);
        ammoMoveDirection = nextPosition - (Vector2)transform.position;
        CalculateNextAmmoMoveSpeed(nextPositionXNormalized);
        transform.position = nextPosition;

        if (nextPositionYNormalized > 0.996)
        {
            ammoState = AmmoState.Linear;
        }
    }

    public Vector2 GetAmmoMoveDirection()
    {
        return ammoMoveDirection;
    }
    public float GetNextYTrajectoryPosition()
    {
        return nextYTrajectoryPosition;
    }

    public float GetNextPositionYCorrectionAbsolute()
    {
        return nextPositionYCorrectionAbsolute;
    }
    private void MoveAmmoByDirection()
    {
        transform.position += (Vector3)ammoMoveDirection.normalized * maxSpeed * Time.deltaTime;
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
        float magnitude = (target - transform.position).magnitude;
        float xDistanceToTarget = target.x - transform.position.x;
        this.trajectoryMaxRelativeHeight = Mathf.Abs(magnitude) * ammoDetailsSO.trajectoryMaxHeight;
        SetTargetPosition(target);
        SetAmmoPlayer();
        InitializeAnimationCurves(trajectoryAniamtionCurve, axisCorrectionAnimationCurve, ammoSpeedAnimationCurve);
        ammoVisual.SetTarget(target);
        gameObject.SetActive(true);
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

        if ((playeLayerMask.value & 1 << collision.gameObject.layer) > 0)
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
        playeLayerMask = LayerMask.GetMask("Player");
    }
    private void DisableAmmo()
    {
        gameObject.SetActive(false);
    }
}


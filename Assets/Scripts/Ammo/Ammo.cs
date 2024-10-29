using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IFireable
{
    [HideInInspector] public AmmoDetailsSO ammoDetailsSO;
    private EnemyEffect enemyEffect;
    private PlayerEffect playerEffect;
    private DestroyedEvent destroyedEvent;
    private AmmoVisual ammoVisual;
    private AmmoAnimation ammoAnimation;
    private LayerMask enemyLayerMask;
    private LayerMask playerLayerMask;
    private LayerMask bossLayerMask;
    private Vector2 trajectoryStartPoint;
    private Vector2 target;
    private Vector2 trajectoryRange;
    private Vector2 ammoMoveDirection;
    private float nextYTrajectoryPosition;
    private float nextXTrajectoryPosition;
    private float trajectoryMaxRelativeHeight;
    private float nextPositionYCorrectionAbsolute;
    private float nextPositionXCorrectionAbsolute;
    private float speed;
    [SerializeField] private int poiseAmount;
    [SerializeField] private float stunDuration;
    public AmmoState ammoState;
    private bool isColliding = false;

    private void Awake()
    {
        enemyEffect = GetComponent<EnemyEffect>();
        playerEffect = GetComponent<PlayerEffect>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        ammoVisual = GetComponent<AmmoVisual>();
        ammoAnimation = GetComponent<AmmoAnimation>();
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
                // last key of trajectory animation should be has a tilt
                MoveAmmoByDirection();
                break;
            case AmmoState.Freeze:
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
            //ammo will be curve on X axis
            UpdateAmmoXPosition();
        }
        else
        {
            //ammo will be curve on Y axis
            if (trajectoryRange.x < 0)
            {
                speed = -speed;
            }
            UpdateAmmoYPosition();
        }
    }

    private void UpdateAmmoXPosition()
    {
        float nextPositionY = transform.position.y + speed * Time.deltaTime;
        float nextPositionYNormalized = (nextPositionY - trajectoryStartPoint.y) / trajectoryRange.y;
        float nextPositionXNormalized = ammoDetailsSO.trajectoryAnimationCurve.Evaluate(nextPositionYNormalized);
        float nextPositionXCorrectionNormalized = ammoDetailsSO.axisCorrectionAnimationCurve.Evaluate(nextPositionYNormalized);
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

    private void UpdateAmmoYPosition()
    {
        float nextPositionX = transform.position.x + speed * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;

        float nextPositionYNormalized = ammoDetailsSO.trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
        float nextPositionYCorrectionNormalized = ammoDetailsSO.axisCorrectionAnimationCurve.Evaluate(nextPositionXNormalized);
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

        if (nextPositionXNormalized > 0.996)
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
        transform.position += (Vector3)ammoMoveDirection.normalized * ammoDetailsSO.maxSpeed * Time.deltaTime;
    }

    private void CalculateNextAmmoMoveSpeed(float nextPositionXNormalized)
    {
        float nextSpeedNormalized = ammoDetailsSO.ammoSpeedAnimationCurve.Evaluate(nextPositionXNormalized);
        speed = ammoDetailsSO.maxSpeed * nextSpeedNormalized;
    }

    public void InitialiseAmmo(AmmoDetailsSO ammoDetailsSO, Vector3 target)
    {
        this.ammoDetailsSO = ammoDetailsSO;
        float magnitude = (target - transform.position).magnitude;
        float xDistanceToTarget = target.x - transform.position.x;
        this.trajectoryMaxRelativeHeight = Mathf.Abs(magnitude) * ammoDetailsSO.trajectoryMaxHeight;
        SetTargetPosition(target);
        SetAmmoPlayer();
        ammoState = AmmoState.Trajectory;
        isColliding = false;
        ammoAnimation.InitializeAmmoAnimation();
        SetGameObjectActive(true);
    }

    private void SetAmmoPlayer()
    {
        gameObject.layer = LayerMask.NameToLayer("EnemyAmmo");
    }

    private void SetTargetPosition(Vector3 target)
    {
        this.target = target;
    }

    private void SetGameObjectActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding)
            return;
        collision.GetComponent<ReceiveDamage>()?.TakeDamage(ammoDetailsSO.damage);
        if ((playerLayerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            collision.GetComponent<PlayerEffect>().DamagePushEfect(transform.position);
            collision.GetComponent<PlayerEffect>().CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
        }

        if ((enemyLayerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            collision.GetComponent<EnemyEffect>()?.DamagePushEfect(transform.position);
            collision.GetComponent<EnemyEffect>()?.CallDamageFlashEffect(GameResources.Instance.damegeFlashMaterial, GameResources.Instance.litMaterial, collision.GetComponentsInChildren<SpriteRenderer>());
            collision.GetComponent<Poise>().TakePoise(poiseAmount);
        }
        if ((bossLayerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            collision.GetComponent<Poise>().TakePoise(poiseAmount);
            collision.GetComponent<BossEffect>()?.PushBossByWeapon(transform.position);
            collision.GetComponent<BossEffect>()?.ouchEffect();
        }
        AmmoHitEffect();
        DisableAmmo();
        isColliding = true;
    }


    private void AmmoHitEffect()
    {
        AmmoHitEffect ammoHitEffect = PoolManager.Instance.ReuseComponent(GameResources.Instance.ammoHitEffectPrefab, transform.position, Quaternion.identity) as AmmoHitEffect;
        ammoHitEffect.InitialiseAmmoHitEffect(ammoDetailsSO.ammoEffectType);
    }

    private void GetLayerMark()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        playerLayerMask = LayerMask.GetMask("Player");
        bossLayerMask = LayerMask.GetMask("Boss");
    }


    private void DisableAmmo()
    {
        gameObject.SetActive(false);
    }
}


using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(AnimateEvent))]
[RequireComponent(typeof(AttackEvent))]
[RequireComponent(typeof(DealDamageEvent))]
[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent(typeof(HealthEvent))]
[RequireComponent(typeof(DestroyedEvent))]
[RequireComponent(typeof(BashEvent))]
[RequireComponent(typeof(DashEvent))]
[RequireComponent(typeof(StaminaEvent))]
[RequireComponent(typeof(Stamina))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerEffect))]
[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    public MovementDetailsSO movementDetails;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public AnimateEvent animateEvent;
    [HideInInspector] public AttackEvent attackEvent;
    [HideInInspector] public DealDamageEvent dealDamageEvent;
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public DestroyedEvent destroyedEvent;
    [HideInInspector] public BashEvent bashEvent;
    [HideInInspector] public DashEvent dashEvent;
    [HideInInspector] public StaminaEvent staminaEvent;
    [HideInInspector] public Stamina stamina;
    [HideInInspector] public Animator animator;
    [HideInInspector] public PlayerEffect playerEffect;
    [HideInInspector] public Rage rage;
    [HideInInspector] public InventoryManager inventoryManager;
    private DieEvent dieEvent;
    [HideInInspector] public Vector2[] spawnPosition = new Vector2[2];
    public SoundEffectSO runSoundEffect;
    public SoundEffectSO dashSoundEffect;
    public SoundEffectSO attackSoundEffect;
    public SoundEffectSO dieSoundEffect;
    private bool isDead;
    private void Awake()
    {
        // Load components
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();
        animateEvent = GetComponent<AnimateEvent>();
        attackEvent = GetComponent<AttackEvent>();
        dealDamageEvent = GetComponent<DealDamageEvent>();
        healthEvent = GetComponent<HealthEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        bashEvent = GetComponent<BashEvent>();
        dashEvent = GetComponent<DashEvent>();
        staminaEvent = GetComponent<StaminaEvent>();
        stamina = GetComponent<Stamina>();
        animator = GetComponent<Animator>();
        playerEffect = GetComponent<PlayerEffect>();
        rage = GetComponent<Rage>();
        inventoryManager = GetComponent<InventoryManager>();
        dieEvent = GetComponent<DieEvent>();
    }

    private void OnEnable()
    {
        StaticEventHandler.CallPlayerChangedEvent(this);
        // Subscribe to player health event
        healthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
        GameManager.Instance.OnGameStateChange += GameStateChanged_OnPlayer;
        dieEvent.OnDie += DieEvent_OnDie;
    }

    private void OnDisable()
    {
        // Unsubscribe from player health event
        healthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
        dieEvent.OnDie -= DieEvent_OnDie;
    }

    public Vector2 GetPlayerPosition()
    {
        return transform.position;
    }
    private void DieEvent_OnDie(DieEvent dieEvent)
    {
        SoundEffectManager.Instance.PlaySoundEffect(dieSoundEffect);
        if (GameManager.Instance.saveFileSetup.GetSaveFile().HasData("Checkpoint"))
        {
            string checkpoint = GameManager.Instance.saveFileSetup.GetSaveFile().GetData<string>("Checkpoint");
            if (checkpoint == "Coast")
            {
                transform.position = spawnPosition[0];
                if (SceneManager.GetSceneAt(1) != null)
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).name);
                }
                SceneManager.LoadScene("Coast", LoadSceneMode.Additive);
            }
            else if (checkpoint == "The Forest")
            {
                transform.position = spawnPosition[1];
                if (SceneManager.GetSceneAt(1) != null)
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).name);
                }
                SceneManager.LoadScene("The Forest", LoadSceneMode.Additive);
            }
        }
        GameResources.Instance.beginUI.SetFloat("_FadeAmount", 0);
        GameManager.Instance.beginSreen.color = new Color(0, 0, 0, 1);
        GameManager.Instance.beginSreen.gameObject.SetActive(true);
        GameManager.Instance.beginSreen.DOFade(0f, 2f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            GameManager.Instance.beginSreen.gameObject.SetActive(false);
            if (GameManager.Instance.gameState == GameState.EngagedBoss)
            {
                MusicManager.Instance.PlayBackgroundMusic();
                GameManager.Instance.HandleGameState(GameState.Play);
            }
        });

    }
    /// <summary>
    /// Handle health changed event
    /// </summary>
    private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        if (healthEventArgs.healthAmount > 0f)
        {
            isDead = true;
        }
        // If player has died
        if (healthEventArgs.healthAmount <= 0f && isDead)
        {

            destroyedEvent.CallDestroyedEvent(new DestroyedEventArgs { playerDied = true });
            isDead = false;
        }
    }
    private void GameStateChanged_OnPlayer(GameState gameState)
    {
        if (gameState == GameState.Intro)
        {
            gameObject.transform.position = spawnPosition[0];
            GameManager.Instance.OnGameStateChange -= GameStateChanged_OnPlayer;
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (boss.isPhaseTwo)
            {
                boss.rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (boss.isPhaseTwo)
            {
                boss.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }


    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(movementDetails), movementDetails);
        HelperUtilities.ValidateCheckNullValue(this, nameof(runSoundEffect), runSoundEffect);
        HelperUtilities.ValidateCheckNullValue(this, nameof(dashSoundEffect), dashSoundEffect);
        HelperUtilities.ValidateCheckNullValue(this, nameof(attackSoundEffect), attackSoundEffect);
        HelperUtilities.ValidateCheckNullValue(this, nameof(dieSoundEffect), dieSoundEffect);
    }


#endif
    #endregion Validation
}

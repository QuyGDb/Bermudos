using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.UI;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class Bash : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Collider2D collider2d;
    private LayerMask layerMask;
    private Player player;
    private Arrow arrow;
    private BashState bashState;
    private bool isDuring;
    private bool isRelease;
    private float aimCountdown;
    private float bashForce = 10f;
    private int bashCost = 10;
    private float bashRadius = 2.5f;
    private GameState gameState;
    [TextArea]
    [SerializeField] private string instructionBash;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        arrow = GetComponent<Arrow>();
        bashState = BashState.None;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += GameStateChanged_OnBash;
        player.bashEvent.OnBash += OnBashEvent_OnBash;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChange -= GameStateChanged_OnBash;
        player.bashEvent.OnBash -= OnBashEvent_OnBash;
    }
    private void GameStateChanged_OnBash(GameState gameState)
    {
        this.gameState = gameState;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((layerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            StopAllCoroutines();
            StartCoroutine(InstructBash());
        }
    }
    private void FixedUpdate()
    {
        if (gameState == GameState.Instruct)
        {
            collider2d = Physics2D.OverlapCircle(transform.position, bashRadius, layerMask.value);
            if (collider2d == null || collider2d.GetComponent<Ammo>() == null) return;
            Time.timeScale = 0f;
            isDuring = true;
            aimCountdown = 10000f;
            StaticEventHandler.CallInstructionChangedEvent(instructionBash);
        }
    }
    private IEnumerator InstructBash()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
    }
    private void OnBashEvent_OnBash(BashEvent bashEvent, BashEventArgs bashEventArgs)
    {
        bashState = bashEventArgs.bashState;
    }

    private void Start()
    {
        layerMask = LayerMask.GetMask("EnemyAmmo");
    }
    private void Update()
    {
        switch (bashState)
        {
            case BashState.ActiveBash:
                Prepare();
                break;
            case BashState.DuringBash:
                if (isDuring)
                    During();
                break;
            case BashState.ReleaseBash:
                if (isRelease)
                    Release();
                break;
            case BashState.None:
                InitializeFlagVarible();
                break;
        }
    }

    private void Prepare()
    {
        player.stamina.UseStamina(bashCost);
        collider2d = Physics2D.OverlapCircle(transform.position, bashRadius, layerMask.value);
        if (collider2d == null || collider2d.GetComponent<Ammo>() == null) return;
        if (player.stamina.currentStamina > bashCost)
        {
            Time.timeScale = 0f;
            isDuring = true;
            aimCountdown = 2f;
        }
    }

    private void During()
    {
        aimCountdown -= Time.unscaledDeltaTime;

        if (aimCountdown <= 0)
        {
            arrow.ClearArrow();
            Time.timeScale = 1f;
            isDuring = false;
        }
        else
        {
            isRelease = true;
            if (collider2d != null)
                arrow.MakeArrow(HelperUtilities.GetMouseWorldPosition(), collider2d.transform.position);
        }
    }

    private void Release()
    {
        Time.timeScale = 1f;
        if (collider2d != null && isRelease)
        {
            collider2d.GetComponent<Ammo>().ammoState = AmmoState.Freeze;
            collider2d.gameObject.layer = LayerMask.NameToLayer("BashAmmo");
            MoveAmmoByBash();

            arrow.ClearArrow();

        }
        bashState = BashState.None;
    }
    private void InitializeFlagVarible()
    {
        isDuring = false;
        isRelease = false;
    }
    private void MoveAmmoByBash()
    {
        collider2d.GetComponent<Rigidbody2D>().AddForce(((Vector2)collider2d.transform.position - (Vector2)HelperUtilities.GetMouseWorldPosition()).normalized * bashForce, ForceMode2D.Impulse);
    }
}



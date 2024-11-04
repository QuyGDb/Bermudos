using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

[RequireComponent(typeof(Player))]
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
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        arrow = GetComponent<Arrow>();
        bashState = BashState.None;
    }

    private void OnEnable()
    {

        player.bashEvent.OnBash += OnBashEvent_OnBash;
    }
    private void OnDisable()
    {

        player.bashEvent.OnBash -= OnBashEvent_OnBash;
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
        collider2d = Physics2D.OverlapCircle(transform.position, 2f, layerMask.value);
        if (collider2d?.GetComponent<Ammo>() == null) return;
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
            bashState = BashState.None;
            arrow.ClearArrow();

        }
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



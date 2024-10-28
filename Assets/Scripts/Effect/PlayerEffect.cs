using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : Effect
{
    private Player player;
    private Coroutine PushPlayerByEnemyCoroutine;
    public bool isDashing = false;
    private float dashEffectDuration = 0.05f;


    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDashing)
            DashShadowPlayerEffect();
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
    public void DamagePushEfect(Vector2 damageSource)
    {
        if (PushPlayerByEnemyCoroutine != null)
        {
            StopCoroutine(PushPlayerByEnemyCoroutine);
        }
        if (gameObject.activeSelf)
        {

            PushPlayerByEnemyCoroutine = StartCoroutine(PushPlayerByEnemy(rb.position + (damageForce * (rb.position - damageSource).normalized)));
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

    private void DashShadowPlayerEffect()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        if (dashEffectDuration > 0)
        {
            dashEffectDuration -= Time.deltaTime;
        }
        else
        {
            PlayerShadow playerShadow = (PlayerShadow)PoolManager.Instance.ReuseComponent(GameResources.Instance.playerShadowPrefab, player.transform.position, Quaternion.identity);
            playerShadow.InitialisePlayerShadow(spriteRenderers);

            dashEffectDuration = 0.035f;
        }

    }
}

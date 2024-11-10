using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerShadow : MonoBehaviour
{
    public void InitialisePlayerShadow(SpriteRenderer[] playerSpriteRenderers)
    {
        EnablePlayerShadow();

        InitialiseSprite(playerSpriteRenderers);
    }
    private void EnablePlayerShadow()
    {
        gameObject.SetActive(true);
    }
    private void DisablePlayerShadow()
    {
        gameObject.SetActive(false);
    }
    public void InitialiseSprite(SpriteRenderer[] playerSpriteRenderers)
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (var spriteRenderer in spriteRenderers)
        {
            foreach (var playerSpriteRenderer in playerSpriteRenderers)
            {
                if (spriteRenderer.name == playerSpriteRenderer.name)
                    spriteRenderer.sprite = playerSpriteRenderer.sprite;
            }
        }
    }
}

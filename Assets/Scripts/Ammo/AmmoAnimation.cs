using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoAnimation : MonoBehaviour
{
    private Animator animator;
    private Ammo ammo;
    [SerializeField] private AnimationClip animationClip;
    private void Awake()
    {
        ammo = GetComponent<Ammo>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        overrideController["MagickShoot"] = ammo.enemyAmmoType;

        // Gán override controller mới vào animator
        animator.runtimeAnimatorController = overrideController;
    }
}

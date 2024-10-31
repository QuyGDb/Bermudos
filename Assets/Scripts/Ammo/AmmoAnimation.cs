using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AmmoAnimation : MonoBehaviour
{
    private Animator animator;
    private Ammo ammo;

    private void Awake()
    {
        ammo = GetComponent<Ammo>();
        animator = GetComponent<Animator>();
    }
    public void InitializeAmmoAnimation()
    {
        AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        overrideController["MagickShoot"] = ammo.ammoDetailsSO.enemyAmmoType;
        // Gán override controller mới vào animator
        animator.runtimeAnimatorController = overrideController;
    }

}

using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

[DisallowMultipleComponent]
public class AnimateBoss : MonoBehaviour
{
    private Boss boss;
    // Start is called before the first frame update

    private void Awake()
    {
        boss = GetComponent<Boss>();
    }
    public void Update()
    {
        SetAimDirection(GameManager.Instance.player.transform.position - transform.position);
    }
    public void InitializeAimDirection()
    {
        boss.animator.SetBool(Settings.aimUp, false);
        boss.animator.SetBool(Settings.aimDown, false);
        boss.animator.SetBool(Settings.aimLeft, false);
        boss.animator.SetBool(Settings.aimRight, false);
    }
    public void SetAimDirection(Vector3 direction)
    {
        float angle = HelperUtilities.GetAngleFromVector(direction);
        if (angle > 45f && angle <= 135f)
        {
            InitializeAimDirection();
            boss.animator.SetTrigger(Settings.aimUp);
        }
        else if (angle > 135f && angle <= 180f || angle > -180f && angle <= -155f)
        {
            InitializeAimDirection();
            boss.animator.SetTrigger(Settings.aimLeft);
        }

        else if (angle > -155f && angle <= -45f)
        {
            InitializeAimDirection();
            boss.animator.SetTrigger(Settings.aimDown);
        }
        else if (angle > -45f && angle <= 45f)
        {
            InitializeAimDirection();
            boss.animator.SetTrigger(Settings.aimRight);
        }
    }

}

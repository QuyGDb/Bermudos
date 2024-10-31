using System.Collections;
using System.Collections.Generic;
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
    public void SetAimDirection(Vector3 direction)
    {
        float angle = HelperUtilities.GetAngleFromVector(direction);
        if (angle > 45f && angle <= 135f)
        {
            boss.animator.SetTrigger(Settings.aimUp);
        }
        else if (angle > 135f && angle <= 180f || angle > -180f && angle <= -155f)
        {
            boss.animator.SetTrigger(Settings.aimLeft);
        }

        else if (angle > -155f && angle <= -45f)
        {
            boss.animator.SetTrigger(Settings.aimDown);
        }
        else if (angle > -45f && angle <= 45f)
        {
            boss.animator.SetTrigger(Settings.aimRight);
        }
    }

}

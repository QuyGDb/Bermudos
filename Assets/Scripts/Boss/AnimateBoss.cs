using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBoss : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetAimDirection(Vector3 direction)
    {

        float angle = HelperUtilities.GetAngleFromVector(direction);
        if (angle > 45f && angle <= 135f)
        {
            animator.SetBool(Settings.aimUp, true);
        }
        else if (angle > 135f && angle <= 180f || angle > -180f && angle <= -155f)
        {
            animator.SetBool(Settings.aimLeft, true);
        }

        else if (angle > -155f && angle <= -45f)
        {
            animator.SetBool(Settings.aimDown, true);
        }
        else if (angle > -45f && angle <= 45f)
        {
            animator.SetBool(Settings.aimRight, true);
        }
    }

}

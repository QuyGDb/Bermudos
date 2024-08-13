using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFireable
{
    void InitialiseAmmo(AmmoDetailsSO ammoDetailsSO, Vector3 target, AnimationCurve trajectoryAnimationCurve, AnimationCurve axisCorrectionAnimationCurve, AnimationCurve ammoSpeedAnimationCurve);
}

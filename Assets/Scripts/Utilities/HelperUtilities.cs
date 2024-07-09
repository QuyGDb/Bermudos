using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static bool ApproximatelyEqual(Vector2 v1, Vector2 v2, float epsilon)
    {
        return Mathf.Abs(v1.x - v2.x) < epsilon && Mathf.Abs(v1.y - v2.y) < epsilon;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoDetailsSO_", menuName = "ScriptableObjects/AmmoDetailsSO", order = 1)]
public class AmmoDetailsSO : ScriptableObject
{
    public float maxSpeed;
    public int damage;
    public float trajectoryMaxHeight;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : MonoBehaviour
{
    public float maxRage;
    public float currentRage;

    public void IncreaseRage(float amount)
    {
        currentRage += amount;
        if (currentRage > maxRage)
            currentRage = maxRage;
    }
}

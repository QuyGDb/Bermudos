using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Poise : MonoBehaviour
{
    private PoiseEvent poiseEvent;
    public float maxPoise;
    public float currentPoise;
    public float stunTime;
    private void Awake()
    {
        poiseEvent = GetComponent<PoiseEvent>();
    }
    private void Start()
    {
        currentPoise = maxPoise;
    }

    public void TakePoise(int poiseAmount)
    {
        currentPoise -= poiseAmount;

        if (currentPoise <= 0)
        {
            poiseEvent.CallPoiseEvent(poiseAmount, currentPoise, maxPoise, stunTime);
        }
    }
}

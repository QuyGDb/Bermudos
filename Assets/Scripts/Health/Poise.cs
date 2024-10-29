using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poise : MonoBehaviour
{
    private PoiseEvent poiseEvent;
    public int maxPoise;
    public int currentPoise;
    public int stunTime;
    private void Awake()
    {
        poiseEvent = GetComponent<PoiseEvent>();
    }
    private void Start()
    {
        currentPoise = maxPoise;
    }

    public void TakePoise(int poisonAmount)
    {
        currentPoise -= poisonAmount;
        if (currentPoise <= 0)
        {
            poiseEvent.CallPoiseEvent(poisonAmount, currentPoise, maxPoise, stunTime);
        }
    }
}

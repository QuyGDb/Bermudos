using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poise : MonoBehaviour
{
    private PoiseEvent poiseEvent;
    private Enemy enemy;
    public int maxPoise;
    private int currentPoise;
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
        poiseEvent.CallPoiseEvent(poisonAmount, currentPoise, maxPoise, stunTime);
    }
}

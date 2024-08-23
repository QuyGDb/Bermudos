using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]
public class StunBar : MonoBehaviour
{
    private Enemy enemy;
    private Slider stunBar;
    private float stunTime;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        stunBar = GetComponentInChildren<Slider>();
    }
    private void OnEnable()
    {
        enemy.poiseEvent.onPoise += PoiseEvent_OnPoise;
    }
    private void OnDisable()
    {
        enemy.poiseEvent.onPoise -= PoiseEvent_OnPoise;
    }

    private void PoiseEvent_OnPoise(PoiseEvent poiseEvent, PoiseEventArgs poiseEventArgs)
    {
        if (poiseEventArgs.currentPoise <= 0)
        {
            stunTime = poiseEventArgs.stunTime;
            stunBar.maxValue = stunTime;
            stunBar.value = stunTime;

        }
    }

    private void Update()
    {
        if (stunBar.value > 0)
            stunBar.value -= Time.deltaTime;
    }
}

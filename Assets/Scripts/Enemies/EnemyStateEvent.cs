using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyStateEvent : MonoBehaviour
{
    public Action<EnemyStateEvent, EnemyStateEventArgs> onEnemyState;

    public void CallEnemyStateEvent(EnemyState enemyState)
    {
        onEnemyState?.Invoke(this, new EnemyStateEventArgs { enemyState = enemyState });
    }
}
public class EnemyStateEventArgs : EventArgs
{
    public EnemyState enemyState;
}

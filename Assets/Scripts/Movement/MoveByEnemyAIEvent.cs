using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByEnemyAIEvent : MonoBehaviour
{
    public event Action<MoveByEnemyAIEvent> OnMoveByEnemyAI;
    public void CallMoveByEnemyAIEvent()
    {
        OnMoveByEnemyAI?.Invoke(this);
    }
}

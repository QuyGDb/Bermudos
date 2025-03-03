﻿
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyStateEvent))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyAttack))]
[DisallowMultipleComponent]
public class EnemyAI : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyStateEvent enemyStateEvent;
    private Enemy enemy;
    [HideInInspector] public EnemyState enemyState;
    private Coroutine attackCoroutine;
    private bool isAttack = true;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemyStateEvent = GetComponent<EnemyStateEvent>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyMovement = GetComponent<EnemyMovement>();

    }
    private void OnEnable()
    {
        enemyStateEvent.onEnemyState += OnEnemyState;
        enemy.poiseEvent.OnPoise += PoiseEvent_OnPoise;

    }
    private void OnDisable()
    {
        enemyStateEvent.onEnemyState -= OnEnemyState;
        enemy.poiseEvent.OnPoise += PoiseEvent_OnPoise;
    }
    private void PoiseEvent_OnPoise(PoiseEvent poiseEvent, PoiseEventArgs poiseEventArgs)
    {
        if (poiseEventArgs.currentPoise <= 0)
        {
            StopAttack(poiseEventArgs.stunTime);
        }
    }
    private void OnEnemyState(EnemyStateEvent enemyStateEvent, EnemyStateEventArgs enemyStateEventArgs)
    {
        enemyState = enemyStateEventArgs.enemyState;
    }

    private void Update()
    {

        switch (enemyState)
        {
            default:
            case EnemyState.Roaming:
                enemy.moveByEnemyAIEvent.CallMoveByEnemyAIEvent();
                enemyMovement.MoveRoamingPosition();
                enemyMovement.FindPlayer();
                enemyMovement.GetAimDirection(false);
                break;
            case EnemyState.Chasing:
                enemy.moveByEnemyAIEvent.CallMoveByEnemyAIEvent();
                enemyMovement.MoveToPlayer();
                enemyMovement.GetDistanceToStartPosition();
                enemyMovement.GetAimDirection(true);
                break;
            case EnemyState.GoBackToStart:
                enemy.moveByEnemyAIEvent.CallMoveByEnemyAIEvent();
                enemyMovement.GoBackToStartPosition();
                // tuy roamposition khong phai starting position nhung 2 ví trí gần nhau nên direction từ current positon đến starting position cũng có tỉ lệ giống với direction từ current position đến roamposition
                enemyMovement.GetAimDirection(false);
                break;
            case EnemyState.Attacking:
                if (!isAttack)
                    return;
                enemy.attackEvent.CallAttackEvent();
                enemyAttack.Shoot();
                enemyStateEvent.CallEnemyStateEvent(EnemyState.Chasing);
                break;
            case EnemyState.None:
                break;
        }
    }
    private void StopAttack(float stunTime)
    {
        StartCoroutine(StopAttackCoroutine(stunTime));
    }
    private IEnumerator StopAttackCoroutine(float stunTime)
    {
        isAttack = false;
        yield return new WaitForSeconds(stunTime);
        isAttack = true;
    }
}

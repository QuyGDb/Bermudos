
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
    private EnemyState enemyState;
    private Coroutine attackCoroutine;
    private bool isAttack = true;
    private void Awake()
    {

        enemyStateEvent = GetComponent<EnemyStateEvent>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyMovement = GetComponent<EnemyMovement>();

    }
    private void OnEnable()
    {
        enemyStateEvent.onEnemyState += OnEnemyState;
    }
    private void OnDisable()
    {
        enemyStateEvent.onEnemyState -= OnEnemyState;
    }


    private void OnEnemyState(EnemyStateEvent enemyStateEvent, EnemyStateEventArgs enemyStateEventArgs)
    {
        enemyState = enemyStateEventArgs.enemyState;
    }

    private IEnumerator AttackCoroutine()
    {
        isAttack = false;
        enemyMovement.navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(1f);
        enemyAttack.Shoot();
        yield return new WaitForSeconds(2f);
        enemyMovement.navMeshAgent.isStopped = false;
        isAttack = true;
    }

    private void Update()
    {

        switch (enemyState)
        {
            default:
            case EnemyState.Roaming:
                enemyMovement.MoveRoamingPosition();
                enemyMovement.FindPlayer();
                enemyMovement.GetAimDirection(false);
                break;
            case EnemyState.Chasing:
                enemyMovement.MoveToPlayer();
                enemyMovement.GetDistanceToStartPosition();
                enemyMovement.GetAimDirection(true);
                break;
            case EnemyState.GoBackToStart:
                enemyMovement.GoBackToStartPosition();
                // tuy roamposition khong phai starting position nhung 2 ví trí gần nhau nên direction từ current positon đến starting position cũng có tỉ lệ giống với direction từ current position đến roamposition
                enemyMovement.GetAimDirection(false);
                break;
            case EnemyState.Attacking:
                if (!isAttack)
                    return;
                if (attackCoroutine != null)
                    StopCoroutine(attackCoroutine);
                attackCoroutine = StartCoroutine(AttackCoroutine());
                break;
        }

    }
}

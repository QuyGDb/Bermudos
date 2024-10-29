using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]
public class EnemyMovement : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 roamingPosition;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    bool hasInitializedRoamingPosition = false;
    private Enemy enemy;
    float maxDistance = 15f;

    private void Awake()
    {
        // Load the components
        enemy = GetComponent<Enemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }
    private void OnEnable()
    {
        enemy.poiseEvent.OnPoise += PoiseEvent_OnPoise;
    }
    private void OnDisable()
    {
        enemy.poiseEvent.OnPoise -= PoiseEvent_OnPoise;
    }


    private void PoiseEvent_OnPoise(PoiseEvent poiseEvent, PoiseEventArgs poiseEventArgs)
    {
        if (poiseEventArgs.currentPoise <= 0)
        {
            StopEnemy(poiseEventArgs.stunTime);
        }
    }
    private void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = 3.0f;
        startPosition = transform.position;
    }

    public void MoveRoamingPosition()
    {


        if (!hasInitializedRoamingPosition)
        {
            roamingPosition = GetRoamingPosition();  // Khởi tạo vị trí roamingPosition lần đầu tiên
            hasInitializedRoamingPosition = true;    // Đánh dấu rằng đã khởi tạo
        }
        navMeshAgent.SetDestination(roamingPosition);
        navMeshAgent.stoppingDistance = 1f;
        float reachedPositionDistance = 1f;
        if (Vector2.Distance(transform.position, roamingPosition) <= reachedPositionDistance)
        {
            roamingPosition = GetRoamingPosition();
        }
    }

    public void FindPlayer()
    {
        float range = 5f;
        if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) < range)
        {
            enemy.enemyStateEvent.CallEnemyStateEvent(EnemyState.Chasing);
        }
    }
    public void MoveToPlayer()
    {
        if (GameManager.Instance.player != null && navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.stoppingDistance = 2f;
            // set the destination of the navmesh agent to the player's position
            navMeshAgent.SetDestination(GameManager.Instance.player.transform.position);
        }
    }

    public void GetDistanceToStartPosition()
    {
        maxDistance = 15f;

        if (Vector2.Distance(transform.position, startPosition) > maxDistance)
        {
            enemy.enemyStateEvent.CallEnemyStateEvent(EnemyState.GoBackToStart);
        }
    }


    public void GoBackToStartPosition()
    {
        if (!navMeshAgent.isOnNavMesh)
            return;
        // reachedPositionDistance phải nhỏ hơn stoppingDistance bằng ở dưới if check chuyển trạng thái so < reachedPositionDistance
        navMeshAgent.stoppingDistance = 2f;
        float reachedPositionDistance = 2f;
        navMeshAgent.SetDestination(startPosition);
        float range = 5f;
        if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) <= range && Vector2.Distance(startPosition, transform.position) <= maxDistance - range) // ? ?i?u ki?n 2, C?n tr? cho range vì n?u enemy ?ang ? trong maxdistance - range
                                                                                                                                                                                         // thì ?i?u ki?n ??u tiên s? ?úng nh?ng sau khi enemy di chuy?n theo player ra kh?i
                                                                                                                                                                                         // maxDistance thì if s? sai enemy ti?p t?c di chuy?n v? startingposition nh?ng sau
                                                                                                                                                                                         // khi di chuy?n vào kho?ng nh? ? maxdistance if l?i ?úng và enemy l?i ?u?i theo player
                                                                                                                                                                                         // d?n ??n enemy b? k?t( di chuy?n v? h??ng player r?i if sai quay l?i di chuy?n v? starting if l?i sai và di chuy?n player)
                                                                                                                                                                                         // khi chuy?n sang tr?ng thái chasing, n?u player ra kh?i vùng maxdistance thì enemy s? quay l?i starting position
                                                                                                                                                                                         // nên c?n tr? ?i range ?? tránh tr??ng h?p này
        {
            enemy.enemyStateEvent.CallEnemyStateEvent(EnemyState.Chasing);
        }
        if (Vector2.Distance(transform.position, startPosition) < reachedPositionDistance)
        {
            enemy.enemyStateEvent.CallEnemyStateEvent(EnemyState.Roaming);
        }
    }
    public Vector2 GetRoamingPosition()
    {
        if (navMeshAgent.isOnNavMesh)
        {
            roamingPosition = (Vector2)startPosition + HelperUtilities.GetRandomDirection() * UnityEngine.Random.Range(1f, 5f);
            NavMeshHit hit;
            NavMesh.SamplePosition(roamingPosition, out hit, 10, 1);

            return hit.position;
        }
        else
        {
            return roamingPosition;
        }
    }

    public void GetAimDirection(bool isPlayerDir)
    {
        Vector3 direction;
        if (isPlayerDir)
        {
            direction = GameManager.Instance.player.transform.position - transform.position;
        }
        else
        {
            direction = (Vector3)roamingPosition - transform.position;
        }

        float angle = HelperUtilities.GetAngleFromVector(direction);
        if (angle > 45f && angle <= 135f)
        {
            enemy.animateEvent.CallAnimateEvent(AimDirection.Up);
        }
        else if (angle > 135f && angle <= 180f || angle > -180f && angle <= -155f)
        {
            enemy.animateEvent.CallAnimateEvent(AimDirection.Left);
        }

        else if (angle > -155f && angle <= -45f)
        {
            enemy.animateEvent.CallAnimateEvent(AimDirection.Down);
        }
        else if (angle > -45f && angle <= 45f)
        {
            enemy.animateEvent.CallAnimateEvent(AimDirection.Right);
        }
    }

    private void StopEnemy(float stunTime)
    {
        enemy.idleEvent.CallIdleEvent();
        StartCoroutine(StopEnemyCoroutine(stunTime));
    }

    private IEnumerator StopEnemyCoroutine(float stunTime)
    {
        navMeshAgent.isStopped = true;
        enemy.enemyAI.enemyState = EnemyState.None;
        yield return new WaitForSeconds(stunTime);
        navMeshAgent.isStopped = false;
        enemy.moveByEnemyAIEvent.CallMoveByEnemyAIEvent();
        enemy.enemyAI.enemyState = EnemyState.Chasing;
    }
}

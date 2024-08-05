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
    private Enemy enemy;
    float maxDistance = 15f;
    private void Awake()
    {
        // Load the components
        enemy = GetComponent<Enemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = 3.0f;
        navMeshAgent.stoppingDistance = 3f;
        startPosition = transform.position;

        // get roaming position
        roamingPosition = GetRoamingPosition();

    }

    public void MoveRoamingPosition()
    {
        navMeshAgent.SetDestination(roamingPosition);
        float reachedPositionDistance = 2f;
        if (Vector2.Distance(transform.position, roamingPosition) < reachedPositionDistance)
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
        if (GameManager.Instance.player != null)
        {
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
        navMeshAgent.SetDestination(startPosition);
        float reachedPositionDistance = 2f;
        float range = 5f;
        if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) <= range && Vector2.Distance(startPosition, transform.position) <= maxDistance - range) // ? ?i?u ki?n 2, C?n tr? cho range v� n?u enemy ?ang ? trong maxdistance - range
                                                                                                                                                                                         // th� ?i?u ki?n ??u ti�n s? ?�ng nh?ng sau khi enemy di chuy?n theo player ra kh?i
                                                                                                                                                                                         // maxDistance th� if s? sai enemy ti?p t?c di chuy?n v? startingposition nh?ng sau
                                                                                                                                                                                         // khi di chuy?n v�o kho?ng nh? ? maxdistance if l?i ?�ng v� enemy l?i ?u?i theo player
                                                                                                                                                                                         // d?n ??n enemy b? k?t( di chuy?n v? h??ng player r?i if sai quay l?i di chuy?n v? starting if l?i sai v� di chuy?n player)
                                                                                                                                                                                         // khi chuy?n sang tr?ng th�i chasing, n?u player ra kh?i v�ng maxdistance th� enemy s? quay l?i starting position
                                                                                                                                                                                         // n�n c?n tr? ?i range ?? tr�nh tr??ng h?p n�y
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
        roamingPosition = (Vector2)startPosition + HelperUtilities.GetRandomDirection() * UnityEngine.Random.Range(1f, 5f);
        NavMeshHit hit;
        NavMesh.SamplePosition(roamingPosition, out hit, 5, 1);
        return hit.position;
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
}
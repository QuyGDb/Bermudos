using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {


        agent.SetDestination(target.position);
    }


}

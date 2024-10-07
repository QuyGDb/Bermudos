using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<Vector2> enemyPostionList;

    private void OnEnable()
    {
        StaticEventHandler.OnBuildNavMesh += StaticEventHandler_OnBuildNavMesh;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnBuildNavMesh -= StaticEventHandler_OnBuildNavMesh;
    }

    private void StaticEventHandler_OnBuildNavMesh()
    {
        foreach (var position in enemyPostionList)
        {
            Instantiate(enemy, position, Quaternion.identity, this.transform);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private EnemyManagerDetailsSO enemyManagerDetailsSO;
    private EnemyManagerData enemyManagerData;
    public List<GameObject> enemyList = new List<GameObject>();

    private void Start()
    {

        if (GameManager.Instance.saveFileSetup.GetSaveFile().HasData(enemyManagerDetailsSO.enemyManagerDataKey))
        {
            enemyManagerData = GameManager.Instance.saveFileSetup.GetSaveFile().GetData<EnemyManagerData>(enemyManagerDetailsSO.enemyManagerDataKey);
        }
    }

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
        int count = 0;
        foreach (var enemyState in enemyManagerData.enemieStateList)
        {


            GameObject newEnemy = Instantiate(enemy, enemyManagerDetailsSO.enemyPostionList[count], Quaternion.Euler(0, 0, 0), this.transform);
            newEnemy.GetComponent<Enemy>().InitializeEnemy(enemyManagerDetailsSO.enemyManagerDataKey, count);
            newEnemy.name = enemyManagerDetailsSO.enemyName + " " + count;
            enemyList.Add(newEnemy);


        }
        count++;

    }
}

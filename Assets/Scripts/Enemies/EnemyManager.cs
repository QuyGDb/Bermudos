using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;

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

    private void StaticEventHandler_OnBuildNavMesh(OnBuildNavMeshEventArgs onBuildNavMeshEventArgs)
    {
        if (onBuildNavMeshEventArgs.isBuildWhenMapChanged)
        {
            int count = 0;
            foreach (var enemyState in enemyManagerData.enemieStateList)
            {
                if (enemyState)
                {
                    GameObject newEnemy = Instantiate(enemy, enemyManagerDetailsSO.enemyPostionList[count], Quaternion.Euler(0, 0, 0), this.transform);
                    newEnemy.GetComponent<Enemy>().InitializeEnemy(enemyManagerDetailsSO.enemyManagerDataKey, count);
                    newEnemy.name = enemyManagerDetailsSO.enemyName + " " + count;
                    enemyList.Add(newEnemy);
                }
                count++;
            }
        }
        else
        {
            //for (int i = 0; i < enemyList.Count; i++)
            //{
            //    if (enemyList[i] == null)
            //    {
            //        GameObject newEnemy = Instantiate(enemy, enemyManagerDetailsSO.enemyPostionList[i], Quaternion.Euler(0, 0, 0), this.transform);
            //        newEnemy.GetComponent<Enemy>().InitializeEnemy(enemyManagerDetailsSO.enemyManagerDataKey, i);
            //        newEnemy.name = enemyManagerDetailsSO.enemyName + " " + i;
            //        enemyList[i] = newEnemy;
            //    }
            //}
            if (enemyList.Count > 0)
            {
                foreach (var enemy in enemyList)
                {
                    Destroy(enemy);
                }
                enemyList.Clear();
            }
            int count = 0;
            foreach (var enemyState in enemyManagerData.enemieStateList)
            {
                GameObject newEnemy = Instantiate(enemy, enemyManagerDetailsSO.enemyPostionList[count], Quaternion.Euler(0, 0, 0), this.transform);
                newEnemy.GetComponent<Enemy>().InitializeEnemy(enemyManagerDetailsSO.enemyManagerDataKey, count);
                newEnemy.name = enemyManagerDetailsSO.enemyName + " " + count;
                enemyList.Add(newEnemy);
                count++;
            }
        }

    }
}

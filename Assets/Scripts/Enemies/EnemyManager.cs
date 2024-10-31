using Esper.ESave;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    // private SaveFileSetup saveFileSetup;
    [SerializeField] private EnemyManagerDetailsSO enemyManagerDetailsSO;
    private EnemyManagerData enemyManagerData;
    //private void Awake()
    //{
    //    saveFileSetup = GetComponent<SaveFileSetup>();
    //}

    private void Start()
    {

        if (GameManager.Instance.saveFileSetup.GetSaveFile().HasData(enemyManagerDetailsSO.enemyManagerDataKey))
        {
            enemyManagerData = GameManager.Instance.saveFileSetup.GetSaveFile().GetData<EnemyManagerData>(enemyManagerDetailsSO.enemyManagerDataKey);
        }
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (saveFileSetup.GetSaveFile().HasData(enemyManagerDetailsSO.enemyManagerDataKey))
    //        {
    //            enemyManagerData = saveFileSetup.GetSaveFile().GetData<EnemyManagerData>(enemyManagerDetailsSO.enemyManagerDataKey);
    //            Debug.Log(enemyManagerDetailsSO.enemyManagerDataKey);
    //            Debug.Log(enemyManagerData.enemieStateList[0]);
    //        }
    //    }
    //}
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
            if (enemyState)
            {

                GameObject newEnemy = Instantiate(enemy, enemyManagerDetailsSO.enemyPostionList[count], Quaternion.identity, this.transform);
                newEnemy.GetComponent<Enemy>().InitializeEnemy(enemyManagerDetailsSO.enemyManagerDataKey, count);
                newEnemy.name = enemyManagerDetailsSO.enemyName + " " + count;
            }
            count++;
        }
    }
}

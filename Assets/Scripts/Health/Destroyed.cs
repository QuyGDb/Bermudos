using Esper.ESave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestroyedEvent))]
[DisallowMultipleComponent]
public class Destroyed : MonoBehaviour
{
    private DestroyedEvent destroyedEvent;
    private Player player;
    private DieEvent dieEvent;
    private void Awake()
    {
        // Load components
        destroyedEvent = GetComponent<DestroyedEvent>();
        player = GetComponent<Player>();
        dieEvent = GetComponent<DieEvent>();
    }

    private void OnEnable()
    {
        //Subscribe to destroyed event
        destroyedEvent.OnDestroyed += DestroyedEvent_OnDestroyed;
    }

    private void OnDisable()
    {
        //Unsubscribe to destroyed event
        destroyedEvent.OnDestroyed -= DestroyedEvent_OnDestroyed;
    }



    private void DestroyedEvent_OnDestroyed(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
    {
        if (destroyedEventArgs.playerDied)
        {
            StartCoroutine(Die());

        }
        else if (destroyedEventArgs.bossDied)
        {
            //StartCoroutine(Die());
        }
        else
        {
            MarkEnemyAsDead();
            Destroy(gameObject);
        }
    }

    private IEnumerator Die()
    {

        yield return new WaitForSeconds(3);

        GameManager.Instance.InitializeEnemyManagerData();

        dieEvent.CallDieEvent();


    }
    private void MarkEnemyAsDead()
    {
        Enemy enemy = GetComponent<Enemy>();
        foreach (var enemyManagerDetails in GameManager.Instance.enemyManagerDetailsSO)
        {
            if (enemy.mapKey == enemyManagerDetails.enemyManagerDataKey)
            {
                if (GameManager.Instance.saveFileSetup.GetSaveFile().HasData(enemyManagerDetails.enemyManagerDataKey))
                {
                    EnemyManagerData enemyManagerData = GameManager.Instance.saveFileSetup.GetSaveFile().GetData<EnemyManagerData>(enemyManagerDetails.enemyManagerDataKey);
                    enemyManagerData.enemieStateList[enemy.enemyID] = false;
                    GameManager.Instance.saveFileSetup.GetSaveFile().AddOrUpdateData(enemyManagerDetails.enemyManagerDataKey, enemyManagerData);
                    GameManager.Instance.saveFileSetup.GetSaveFile().Save();
                    Debug.Log("Enemy Marked as dead" + enemy.enemyID);
                }

            }
        }
    }
}

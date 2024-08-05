using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private AmmoDetailsSO ammoDetailsSO;
    [SerializeField] private GameObject ammoPrefab;
    private float attackDistance = 6f;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    private void Start()
    {
        InvokeRepeating("CallEnemyAttackState", 1f, Random.Range(3, 5f));
    }

    public void Shoot()
    {
        // Shoot
        IFireable fireable = (IFireable)PoolManager.Instance.ReuseComponent(ammoPrefab, shootPosition.position, Quaternion.identity);
        fireable.InitialiseAmmo(ammoDetailsSO, (GameManager.Instance.player.transform.position - transform.position).normalized);
        enemy.enemyStateEvent.CallEnemyStateEvent(EnemyState.Chasing);

    }

    private void CallEnemyAttackState()
    {
        if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) < attackDistance)
        {
            enemy.enemyStateEvent.CallEnemyStateEvent(EnemyState.Attacking);
        }

    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(shootPosition), shootPosition);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoDetailsSO), ammoDetailsSO);
        HelperUtilities.ValidateCheckNullValue(this, nameof(ammoPrefab), ammoPrefab);
    }
#endif
    #endregion
}
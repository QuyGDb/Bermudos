using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyAttack : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private AmmoDetailsSO ammoDetailsSO;
    [SerializeField] private float attackDistance = 8f;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    private void Start()
    {
        InvokeRepeating("CallEnemyAttackState", 3f, Random.Range(2f, 3f));
    }

    public void Shoot()
    {
        // Shoot

        IFireable fireable = (IFireable)PoolManager.Instance.ReuseComponent(ammoDetailsSO.ammoPrefab, shootPosition.position, Quaternion.identity);
        fireable?.InitialiseAmmo(ammoDetailsSO, GameManager.Instance.player.transform.position);

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

    }
#endif
    #endregion
}

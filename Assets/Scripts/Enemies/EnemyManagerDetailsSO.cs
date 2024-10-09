using Esper.ESave;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManagerDetailsSO_", menuName = "ScriptableObjects/Enemy/EnemyManagerDetailsSO", order = 1)]
public class EnemyManagerDetailsSO : ScriptableObject
{
    public GameObject enemyPrefab;
    public string enemyName;
    public List<Vector2> enemyPostionList;
    public string enemyManagerDataKey;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(enemyPrefab), enemyPrefab);
        HelperUtilities.ValidateCheckEmptyString(this, nameof(enemyName), enemyName);
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(enemyPostionList), enemyPostionList);
        HelperUtilities.ValidateCheckEmptyString(this, nameof(enemyManagerDataKey), enemyManagerDataKey);
    }
#endif
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransitionPointDetails_", menuName = "ScriptableObjects/TransitionPointDetailsSO", order = 1)]
public class TransitionPointDetailsSO : ScriptableObject
{
    public string transitionPointName;
    public string transitionMap;
    public Vector2 playerSpawnPoint;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEmptyString(this, nameof(transitionPointName), transitionPointName);
        HelperUtilities.ValidateCheckEmptyString(this, nameof(transitionMap), transitionMap);

    }
#endif
    #endregion
}

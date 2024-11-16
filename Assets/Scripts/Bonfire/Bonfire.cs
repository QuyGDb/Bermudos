using Esper.ESave;
using UnityEngine;
using UnityEngine.UI;

public class Bonfire : MonoBehaviour
{
    private LayerMask playerLayer;
    [SerializeField] private SpriteRenderer icon;
    private void Awake()
    {
        playerLayer = LayerMask.GetMask("Player");
        icon.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StaticEventHandler.OnPressRestEvent += RestInBonfire;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnPressRestEvent -= RestInBonfire;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((playerLayer.value & 1 << collision.gameObject.layer) > 0)
        {
            icon.gameObject.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        icon.gameObject.SetActive(false);
    }
    public void RestInBonfire()
    {
        if (icon.gameObject.activeSelf)
        {
            StaticEventHandler.CallRestInBonfireEvent();
            SaveCheckPointAtBonfire();

        }
    }
    private void SaveCheckPointAtBonfire()
    {
        GameManager.Instance.saveFileSetup.GetSaveFile().AddOrUpdateData("Checkpoint", "The Forest");
        GameManager.Instance.saveFileSetup.GetSaveFile().Save();
    }
    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(icon), icon);
    }
#endif
    #endregion
}

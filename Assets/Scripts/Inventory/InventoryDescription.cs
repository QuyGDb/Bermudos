
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class InventoryDescription : MonoBehaviour
{
    private ItemUI itemUI;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private void OnEnable()
    {
        StaticEventHandler.OnItemUIClickChanged += StaticEventHandler_OnItemUIClickChanged;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnItemUIClickChanged -= StaticEventHandler_OnItemUIClickChanged;
    }

    private void StaticEventHandler_OnItemUIClickChanged(OnItemUIChangedEventArgs onItemUIChangedEventArgs)
    {
        itemUI = onItemUIChangedEventArgs.itemUI;
        if (itemUI != null)
        {
            image.gameObject.SetActive(true);
            image.sprite = itemUI.inventoryItem.itemSO.itemIcon;
            titleText.text = itemUI.inventoryItem.itemSO.itemName;

            descriptionText.text = itemUI.inventoryItem.itemSO.itemDescription;
        }
    }
}

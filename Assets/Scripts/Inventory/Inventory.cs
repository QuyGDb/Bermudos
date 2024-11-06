using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [HideInInspector] public List<ItemUI> inventorySlotList = new();
    [HideInInspector] public List<ItemUI> hotBarSlotList = new();
    public GameObject itemUIPrefab;
    public GameObject content;
    public GameObject hotBar;
    public ItemUI draggedItemUI;
    public ItemUI hoverItemUI;
    private void Awake()
    {
        StaticEventHandler.CallInventoryChangedEvent(this);
    }
    private void OnEnable()
    {
        StaticEventHandler.OnItemUIEndDragChanged += StaticEventHandler_OnItemUIChanged;
        StaticEventHandler.OnItemUIHoverChanged += StaticEventHandler_OnItemUIHoverChanged;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnItemUIEndDragChanged -= StaticEventHandler_OnItemUIChanged;
        StaticEventHandler.OnItemUIHoverChanged -= StaticEventHandler_OnItemUIHoverChanged;
    }
    private void StaticEventHandler_OnItemUIChanged(OnItemUIChangedEventArgs onItemUIChangedEventArgs)
    {
        draggedItemUI = onItemUIChangedEventArgs.itemUI;
    }
    private void StaticEventHandler_OnItemUIHoverChanged(OnItemUIChangedEventArgs onItemUIChangedEventArgs)
    {
        if (draggedItemUI != null)
        {
            hoverItemUI = onItemUIChangedEventArgs.itemUI;
            SwapItemUI();
            // draggedItemUI = null;
        }

    }
    public void SwapItemUI()
    {
        if (draggedItemUI != hoverItemUI)
        {
            if (hoverItemUI.isHasItem)
            {

                var temp = draggedItemUI;
                draggedItemUI = hoverItemUI;
                draggedItemUI.ItemImg = hoverItemUI.ItemImg;
                draggedItemUI.inventoryItem = hoverItemUI.inventoryItem;
                draggedItemUI.quantityText.text = hoverItemUI.quantityText.text;
                hoverItemUI = temp;
                hoverItemUI.ItemImg = temp.ItemImg;
                hoverItemUI.inventoryItem = temp.inventoryItem;
                hoverItemUI.quantityText.text = temp.quantityText.text;

            }
            if (!hoverItemUI.isHasItem)
            {
                Debug.Log("Swap1");
                hoverItemUI.ItemImg = draggedItemUI.ItemImg;
                hoverItemUI.inventoryItem = draggedItemUI.inventoryItem;
                hoverItemUI.quantityText.text = draggedItemUI.quantityText.text;
                hoverItemUI.isHasItem = true;
                draggedItemUI.ItemImg = null;
                draggedItemUI.inventoryItem = null;
                draggedItemUI.quantityText.text = "";
                draggedItemUI.isHasItem = false;

            }
        }
    }
    public void InitializeInventorySlotList(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject slot = Instantiate(itemUIPrefab);
            slot.transform.SetParent(content.transform, false);
            slot.name = "Slot " + i;
            ItemUI newItemUI = slot.GetComponent<ItemUI>();
            newItemUI.itemId = i;
            newItemUI.ItemUIType = ItemUIType.Inventory;
            inventorySlotList.Add(newItemUI);
        }
    }
    public void InitializeHotBarSlotList(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject slot = Instantiate(itemUIPrefab);
            slot.transform.SetParent(hotBar.transform, false);
            slot.name = "Slot " + i;
            ItemUI newItemUI = slot.GetComponent<ItemUI>();
            newItemUI.itemId = i;
            newItemUI.ItemUIType = ItemUIType.HotBar;
            hotBarSlotList.Add(newItemUI);
        }
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(itemUIPrefab), itemUIPrefab);
        HelperUtilities.ValidateCheckNullValue(this, nameof(content), content);
        HelperUtilities.ValidateCheckNullValue(this, nameof(hotBar), hotBar);
    }
#endif
    #endregion
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Inventory : MonoBehaviour
{
    [HideInInspector] public List<ItemUI> inventorySlotList = new();
    [HideInInspector] public List<ItemUI> hotBarSlotList = new();
    public GameObject itemUIPrefab;
    public GameObject content;
    public GameObject hotBar;
    private ItemUI draggedItemUI;
    private ItemUI hoverItemUI;
    private ItemUI pressedItemUI;
    private ItemUI previousPressedItemUI;

    private void Start()
    {
        StaticEventHandler.CallInventoryChangedEvent(this);
    }
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        DOTween.Kill(this.transform);
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        StaticEventHandler.OnItemUIEndDragChanged += StaticEventHandler_OnItemUIChanged;
        StaticEventHandler.OnItemUIHoverChanged += StaticEventHandler_OnItemUIHoverChanged;
        StaticEventHandler.OnItemUIClickChanged += StaticEventHandler_OnItemUIClickChanged;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnItemUIEndDragChanged -= StaticEventHandler_OnItemUIChanged;
        StaticEventHandler.OnItemUIHoverChanged -= StaticEventHandler_OnItemUIHoverChanged;
        StaticEventHandler.OnItemUIClickChanged -= StaticEventHandler_OnItemUIClickChanged;
    }
    private void OnDestroy()
    {
        DOTween.Kill(this.transform);
    }
    public void ResetInventory(List<InventoryItem> skipList)
    {
        foreach (var itemUI in inventorySlotList)
        {
            if (itemUI.inventoryItem != default && skipList.Contains(itemUI.inventoryItem))
            {
                continue;
            }
            itemUI.ResetItemUI();
        }
        foreach (var itemUI in hotBarSlotList)
        {
            if (itemUI.inventoryItem != default && skipList.Contains(itemUI.inventoryItem))
            {
                continue;
            }
            itemUI.ResetItemUI();
        }
    }

    private void StaticEventHandler_OnItemUIClickChanged(OnItemUIChangedEventArgs onItemUIChangedEventArgs)
    {
        previousPressedItemUI = pressedItemUI;
        pressedItemUI = onItemUIChangedEventArgs.itemUI;
        if (previousPressedItemUI == null)
        {
            pressedItemUI.SelectorImg.gameObject.SetActive(true);
        }
        else
        if (previousPressedItemUI != pressedItemUI)
        {
            pressedItemUI.SelectorImg.gameObject.SetActive(true);
            previousPressedItemUI.SelectorImg.gameObject.SetActive(false);
        }
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
            MoveAndSwapItemUI();
            draggedItemUI = null;
        }

    }
    public void MoveAndSwapItemUI()
    {
        if (draggedItemUI == hoverItemUI)
            return;

        if (draggedItemUI.ItemUIType == ItemUIType.Inventory && hoverItemUI.ItemUIType == ItemUIType.HotBar)
        {
            Debug.Log(draggedItemUI.inventoryItem.isOnHotBar);

            if (draggedItemUI.inventoryItem.isOnHotBar)
                return;
            MoveItemToHotBar();
            StaticEventHandler.CallMoveItemToHotBarEvent(hoverItemUI.inventoryItem);
        }

        else if (draggedItemUI.ItemUIType == ItemUIType.HotBar && hoverItemUI.ItemUIType == ItemUIType.Inventory)
        {
            return;
        }
        else if (draggedItemUI.ItemUIType == ItemUIType.Inventory && hoverItemUI.ItemUIType == ItemUIType.Inventory)
        {
            if (hoverItemUI.isHasItem)
            {

                SwapTwoItemUI(ItemUIType.Inventory);
            }
            else
            {
                SwapItemUIWithEmptySlot(ItemUIType.Inventory);
                draggedItemUI.ItemImg.gameObject.SetActive(false);
                draggedItemUI.quantityText.gameObject.SetActive(false);
                draggedItemUI.isHasItem = false;
            }
        }
        else
        {
            if (hoverItemUI.isHasItem)
            {

                SwapTwoItemUI(ItemUIType.HotBar);
            }
            else
            {
                SwapItemUIWithEmptySlot(ItemUIType.HotBar);
            }
        }

    }
    public void SwapTwoItemUI(ItemUIType itemUIType)
    {
        var tempItemImg = draggedItemUI.ItemImg.sprite;
        var tempQuantityText = draggedItemUI.quantityText.text;
        var tempInventoryItem = draggedItemUI.inventoryItem;
        draggedItemUI.ItemImg.sprite = hoverItemUI.ItemImg.sprite;
        draggedItemUI.quantityText.text = hoverItemUI.quantityText.text;
        draggedItemUI.inventoryItem = hoverItemUI.inventoryItem;
        Debug.Log(draggedItemUI.inventoryItem.inventorySlot + draggedItemUI.inventoryItem.itemSO.itemName);
        if (itemUIType == ItemUIType.Inventory)
            draggedItemUI.inventoryItem.inventorySlot = draggedItemUI.itemId;
        else
            draggedItemUI.inventoryItem.hotbarSlot = draggedItemUI.itemId;
        hoverItemUI.ItemImg.sprite = tempItemImg;
        hoverItemUI.quantityText.text = tempQuantityText;
        hoverItemUI.inventoryItem = tempInventoryItem;
        if (itemUIType == ItemUIType.Inventory)
            hoverItemUI.inventoryItem.inventorySlot = hoverItemUI.itemId;
        else
            hoverItemUI.inventoryItem.hotbarSlot = hoverItemUI.itemId;

    }
    public void SwapItemUIWithEmptySlot(ItemUIType itemUITypee)
    {
        hoverItemUI.ItemImg.gameObject.SetActive(true);
        hoverItemUI.quantityText.gameObject.SetActive(true);
        hoverItemUI.ItemImg.sprite = draggedItemUI.ItemImg.sprite;
        hoverItemUI.quantityText.text = draggedItemUI.quantityText.text;
        hoverItemUI.inventoryItem = draggedItemUI.inventoryItem;
        if (itemUITypee == ItemUIType.Inventory)
            hoverItemUI.inventoryItem.inventorySlot = hoverItemUI.itemId;
        else
            hoverItemUI.inventoryItem.hotbarSlot = hoverItemUI.itemId;
        hoverItemUI.isHasItem = true;
        draggedItemUI.ItemImg.gameObject.SetActive(false);
        draggedItemUI.quantityText.gameObject.SetActive(false);
        draggedItemUI.isHasItem = false;
        draggedItemUI.inventoryItem = default;
    }
    public void MoveItemToHotBar()
    {
        hoverItemUI.ItemImg.gameObject.SetActive(true);
        hoverItemUI.quantityText.gameObject.SetActive(true);
        hoverItemUI.ItemImg.sprite = draggedItemUI.ItemImg.sprite;
        hoverItemUI.quantityText.text = draggedItemUI.quantityText.text;
        hoverItemUI.inventoryItem = draggedItemUI.inventoryItem;
        hoverItemUI.inventoryItem.hotbarSlot = hoverItemUI.itemId;
        hoverItemUI.inventoryItem.isOnHotBar = true;
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
            newItemUI.inventoryItem = default;
            newItemUI.isHasItem = false;
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
            newItemUI.inventoryItem = default;
            newItemUI.isHasItem = false;
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

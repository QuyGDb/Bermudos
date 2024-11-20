using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class HotBarScroll : MonoBehaviour
{
    [SerializeField] private Image itemIcom;
    private TextMeshProUGUI itemQuantity;
    private int currentIndex;
    private Sprite emptyItemSprite;
    private void Awake()
    {
        itemQuantity = GetComponentInChildren<TextMeshProUGUI>();
        StaticEventHandler.OnItemChanged += StaticEventHandler_OnItemChanged;
        StaticEventHandler.OnMoveItemToHotBar += StaticEventHandler_OnMoveItemToHotBar;
        GameManager.Instance.OnGameStateChange += OnGameStateChange_HotBar;
    }


    private void OnDestroy()
    {
        StaticEventHandler.OnItemChanged -= StaticEventHandler_OnItemChanged;
        StaticEventHandler.OnMoveItemToHotBar -= StaticEventHandler_OnMoveItemToHotBar;
    }
    private void OnGameStateChange_HotBar(GameState gameState)
    {

        if (gameState == GameState.Play || gameState == GameState.Instruct)
        {
            gameObject.SetActive(true);
            emptyItemSprite = itemIcom.sprite;
            if (GameManager.Instance.player.inventoryManager.HotBarItem.Count == 0)
            {
                return;
            }
            currentIndex = 0;
            itemIcom.sprite = GameManager.Instance.player.inventoryManager.HotBarItem[currentIndex].itemSO.itemIcon;
            itemQuantity.text = GameManager.Instance.player.inventoryManager.HotBarItem[currentIndex].quantity.ToString();
            GameManager.Instance.OnGameStateChange -= OnGameStateChange_HotBar;

        }
    }
    private void StaticEventHandler_OnItemChanged(OnInventoryItemChangedEventArgs onInventoryItemChangedEventArgs)
    {
        if (GameManager.Instance.player.inventoryManager.HotBarItem.Count == 0)
        {
            return;
        }
        if (GameManager.Instance.player.inventoryManager.HotBarItem[currentIndex] == null)
            return;
        if (onInventoryItemChangedEventArgs.inventoryItem.hotbarSlot != GameManager.Instance.player.inventoryManager.HotBarItem[currentIndex].hotbarSlot)
            return;
        itemIcom.sprite = onInventoryItemChangedEventArgs.inventoryItem.itemSO.itemIcon;
        itemQuantity.text = onInventoryItemChangedEventArgs.inventoryItem.quantity.ToString();
        if (onInventoryItemChangedEventArgs.inventoryItem.quantity == 0)
        {
            itemIcom.sprite = emptyItemSprite;
            itemQuantity.text = "";
        }
    }
    private void StaticEventHandler_OnMoveItemToHotBar(OnInventoryItemChangedEventArgs onInventoryItemChangedEventArgs)
    {
        if (GameManager.Instance.player.inventoryManager.HotBarItem.Count == 1)
        {
            itemIcom.sprite = onInventoryItemChangedEventArgs.inventoryItem.itemSO.itemIcon;
            itemQuantity.text = onInventoryItemChangedEventArgs.inventoryItem.quantity.ToString();
        }

    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Mouse.current != null)
        {
            Vector2 scrollValue = Mouse.current.scroll.ReadValue();
            if (scrollValue.y != 0)
                ChangedItem((int)scrollValue.y);

        }
    }
    private void ChangedItem(int scrollValue)
    {

        if (scrollValue < 0)
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = GameManager.Instance.player.inventoryManager.HotBarItem.Count - 1;
            if (GameManager.Instance.player.inventoryManager.HotBarItem.Count == 0)
                return;
            itemIcom.sprite = GameManager.Instance.player.inventoryManager.HotBarItem[currentIndex].itemSO.itemIcon;
            itemQuantity.text = GameManager.Instance.player.inventoryManager.HotBarItem[currentIndex].quantity.ToString();
        }
        else
        {
            currentIndex++;
            if (currentIndex > GameManager.Instance.player.inventoryManager.HotBarItem.Count - 1)
                currentIndex = 0;
            if (GameManager.Instance.player.inventoryManager.HotBarItem.Count == 0)
                return;
            itemIcom.sprite = GameManager.Instance.player.inventoryManager.HotBarItem[currentIndex].itemSO.itemIcon;
            itemQuantity.text = GameManager.Instance.player.inventoryManager.HotBarItem[currentIndex].quantity.ToString();
        }
        StaticEventHandler.CallHotBarScrollChangedEvent(GameManager.Instance.player.inventoryManager.HotBarItem[currentIndex]);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(itemIcom), itemIcom);
    }
#endif
    #endregion
}

using Esper.ESave;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class InventoryManager : MonoBehaviour
{

    public List<InventoryItem> inventoryItemList = new List<InventoryItem>();
    public List<InventoryItem> HotBarItem
    {
        get
        {
            return inventoryItemList.Where(x => x.hotbarSlot > -1)
                                .OrderBy(x => x.hotbarSlot)
                                .ToList();
        }
    }
    public List<ItemSO> itemSOList = new List<ItemSO>();
    private Inventory inventory;
    private SaveFileSetup saveFileSetup;
    private SaveFile saveFile;
    public Image inventoryInstructionImg;
    private string instructionText = "Drag the item to the hotbar to use it.";
    private bool isInstruction;
    private void Awake()
    {
        saveFileSetup = GetComponent<SaveFileSetup>();
        saveFile = saveFileSetup.GetSaveFile();
    }
    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        StaticEventHandler.OnInventoryChanged += StaticEventHandler_OnInventoryChanged;
        StaticEventHandler.OnExit += StaticEventHandler_OnExit;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChange -= Instance_OnGameStateChange;
        StaticEventHandler.OnInventoryChanged -= StaticEventHandler_OnInventoryChanged;
        StaticEventHandler.OnExit -= StaticEventHandler_OnExit;
    }

    private void Instance_OnGameStateChange(GameState gameState)
    {
        if (gameState == GameState.Instruct)
        {
            isInstruction = true;
            GameManager.Instance.OnGameStateChange -= Instance_OnGameStateChange;
        }

        if (gameState == GameState.Play)
        {
            LoadItemFromJson();
            GameManager.Instance.OnGameStateChange -= Instance_OnGameStateChange;
        }

    }
    private void StaticEventHandler_OnInventoryChanged(OnInventoryChangedEventArgs onInventoryChangedEventArgs)
    {
        inventory = onInventoryChangedEventArgs.inventory;
        inventory.gameObject.SetActive(false);
        inventory.InitializeInventorySlotList(Settings.inventorySlotQuantity);
        inventory.InitializeHotBarSlotList(Settings.hotBarSlotQuantity);
    }

    private void Start()
    {

    }

    public void ToggleInventory()
    {
        if (inventory.gameObject.activeSelf)
        {
            inventory.gameObject.SetActive(false);
            if (HotBarItem.Count > 0)
                isInstruction = false;
            StaticEventHandler.CallInstructionChangedEvent("", -9, false);
        }
        else
        {
            inventory.ResetInventory(inventoryItemList);
            LoadItemsToInventory();
            if (isInstruction && inventoryItemList.Count > 0)
                StaticEventHandler.CallInstructionChangedEvent(instructionText);
            inventory.gameObject.SetActive(true);
        }
    }
    private void LoadItemsToInventory()
    {
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            var inventoryItem = inventoryItemList[i];
            if (inventoryItem.inventorySlot >= 0)
            {
                SetInventorySlot(inventory.inventorySlotList[inventoryItem.inventorySlot], inventoryItem);
            }
            else if (inventoryItem.inventorySlot < 0)
            {
                foreach (ItemUI itemUI in inventory.inventorySlotList)
                {
                    if (!itemUI.isHasItem)
                    {
                        SetInventorySlot(itemUI, inventoryItem);
                        inventoryItem.inventorySlot = itemUI.itemId;
                        break;
                    }
                }
            }
            if (inventoryItem.hotbarSlot >= 0)
            {
                SetInventorySlot(inventory.hotBarSlotList[inventoryItem.hotbarSlot], inventoryItem);
            }

        }
    }

    private void SetInventorySlot(ItemUI slot, InventoryItem item)
    {
        slot.ItemImg.gameObject.SetActive(true);
        slot.quantityText.gameObject.SetActive(true);
        slot.ItemImg.sprite = item.itemSO.itemIcon;
        slot.quantityText.text = item.quantity.ToString();
        slot.inventoryItem = item;
        slot.isHasItem = true;
    }

    public void CollectIntentoryItem(ItemSO itemSO)
    {
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            InventoryItem inventoryItem = inventoryItemList[i];
            if (inventoryItem.itemSO == itemSO)
            {
                if (inventoryItem.quantity >= Settings.maxStack)
                    return;
                inventoryItem.quantity++;
                StaticEventHandler.CallItemChangedEvent(inventoryItem);
                return;
            }
        }
        InventoryItem newInventoryItem = new InventoryItem();
        newInventoryItem.itemSO = itemSO;
        newInventoryItem.quantity = 1;
        newInventoryItem.inventorySlot = -1;
        newInventoryItem.hotbarSlot = -1;
        inventoryItemList.Add(newInventoryItem);
    }

    public void LoadItemFromJson()
    {
        foreach (var itemSO in itemSOList)
        {
            if (saveFile.HasData(itemSO.itemName))
            {
                ItemJson itemJson = saveFile.GetData<ItemJson>(itemSO.itemName);
                InventoryItem inventoryItem = new InventoryItem();
                inventoryItem.itemSO = itemSO;
                inventoryItem.quantity = itemJson.quantity;
                inventoryItem.inventorySlot = itemJson.inventorySlot;
                inventoryItem.hotbarSlot = itemJson.hotbarSlot;
                inventoryItem.isOnHotBar = itemJson.isOnHotBar;
                inventoryItemList.Add(inventoryItem);
            }
        }
    }
    private void SaveItemToJson()
    {
        saveFile.DeleteFile();
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            InventoryItem inventoryItem = inventoryItemList[i];
            ItemJson itemJson = new ItemJson(inventoryItem.quantity, inventoryItem.inventorySlot, inventoryItem.hotbarSlot, inventoryItem.isOnHotBar);
            saveFile.AddOrUpdateData(inventoryItem.itemSO.itemName, itemJson);
        }
        saveFile.Save();

#if UNITY_WEBGL

        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            InventoryItem inventoryItem = inventoryItemList[i];
            ItemJson itemJson = new ItemJson(inventoryItem.quantity, inventoryItem.inventorySlot, inventoryItem.hotbarSlot, inventoryItem.isOnHotBar);
            string data = JsonUtility.ToJson(itemJson);
            PlayerPrefs.SetString(inventoryItem.itemSO.itemName, data);
        }
#endif
    }
    private void OnApplicationQuit()
    {
        SaveItemToJson();
    }

    private void StaticEventHandler_OnExit()
    {
        SaveItemToJson();
    }

}
using Esper.ESave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] private List<InventoryItem> inventoryList = new List<InventoryItem>();
    private List<ItemJson> itemJsonList = new List<ItemJson>();
    public List<ItemSO> itemSOList = new List<ItemSO>();
    private Inventory inventory;
    private SaveFileSetup saveFileSetup;
    private SaveFile saveFile;
    private bool isFirstTimeOpenInventory = true;
    private void Awake()
    {
        saveFileSetup = GetComponent<SaveFileSetup>();
        saveFile = saveFileSetup.GetSaveFile();
    }
    private void OnEnable()
    {
        StaticEventHandler.OnInventoryChanged += StaticEventHandler_OnInventoryChanged;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnInventoryChanged -= StaticEventHandler_OnInventoryChanged;
    }

    private void StaticEventHandler_OnInventoryChanged(OnInventoryChangedEventArgs onInventoryChangedEventArgs)
    {
        inventory = onInventoryChangedEventArgs.inventory;
        inventory.gameObject.SetActive(false);
    }
    private void Start()
    {
        LoadItemFromJson();
    }
    public void ToggleInventory()
    {
        if (inventory.gameObject.activeSelf)
        {
            inventory.gameObject.SetActive(false);
        }
        else
        {
            if (isFirstTimeOpenInventory)
            {
                inventory.InitializeInventorySlotList(Settings.inventorySlotQuantity);
                inventory.InitializeHotBarSlotList(Settings.hotBarSlotQuantity);
                isFirstTimeOpenInventory = false;
            }
            LoadItemsToInventory();
            inventory.gameObject.SetActive(true);
        }
    }
    private void LoadItemsToInventory()
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            Debug.Log(inventoryList[i].item.itemName);
            var inventoryItem = inventoryList[i];
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
        }
    }

    private void SetInventorySlot(ItemUI slot, InventoryItem item)
    {
        slot.ItemImg.gameObject.SetActive(true);
        slot.quantityText.gameObject.SetActive(true);
        slot.ItemImg.sprite = item.item.itemIcon;
        slot.quantityText.text = item.quantity.ToString();
        slot.inventoryItem = item;
        slot.isHasItem = true;
    }
    public void CollectIntentoryItem(ItemSO item)
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            InventoryItem inventoryItem = inventoryList[i];
            if (inventoryItem.item == item)
            {
                inventoryItem.quantity++;
                return;
            }
        }

        InventoryItem newInventoryItem = new InventoryItem();
        newInventoryItem.item = item;
        newInventoryItem.quantity = 1;
        newInventoryItem.inventorySlot = -1;
        newInventoryItem.hotbarSlot = -1;
        inventoryList.Add(newInventoryItem);
    }
    public void LoadItemFromJson()
    {
        foreach (var itemSO in itemSOList)
        {
            if (saveFile.HasData(itemSO.itemName))
            {
                ItemJson itemJson = saveFile.GetData<ItemJson>(itemSO.itemName);
                InventoryItem inventoryItem = new InventoryItem();
                inventoryItem.item = itemSO;
                inventoryItem.quantity = itemJson.quantity;
                inventoryItem.inventorySlot = itemJson.inventorySlot;
                inventoryItem.hotbarSlot = itemJson.hotbarSlot;
                inventoryList.Add(inventoryItem);
            }

        }
    }
    private void SaveItemToJson()
    {
        if (inventoryList.Count == 0)
        {
            saveFile.DeleteFile();
            return;
        }
        for (int i = 0; i < inventoryList.Count; i++)
        {
            InventoryItem inventoryItem = inventoryList[i];
            ItemJson itemJson = new ItemJson(inventoryItem.quantity, inventoryItem.inventorySlot, inventoryItem.hotbarSlot);
            saveFile.AddOrUpdateData(inventoryItem.item.itemName, itemJson);
        }
        saveFile.Save();
    }
    private void OnApplicationQuit()
    {
        SaveItemToJson();
    }

}
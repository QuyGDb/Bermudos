using Esper.ESave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public struct InventoryItem
    {
        public ItemSO item;
        public int quantity;
    }
    [SerializeField] private List<InventoryItem> inventoryList = new List<InventoryItem>();

    private SaveFileSetup saveFileSetup;
    private void Awake()
    {
        saveFileSetup = GetComponent<SaveFileSetup>();
    }

    private void Start()
    {
        LoadItemFromJson();
    }
    private void LoadItemFromJson()
    {
    }
}

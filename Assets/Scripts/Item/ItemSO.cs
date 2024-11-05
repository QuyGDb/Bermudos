using UnityEngine;


[CreateAssetMenu(fileName = "Item_", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefabs;
    public Sprite itemIcon;
    public string itemDescription;
    public float dropRate;
    public ItemType itemType;
}
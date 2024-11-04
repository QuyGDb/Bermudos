using UnityEngine;


[CreateAssetMenu(fileName = "Item_", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefabs;
    public float dropRate;
}
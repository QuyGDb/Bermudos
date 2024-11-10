using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ItemDropper : MonoBehaviour
{
    [SerializeField] private List<ItemSO> itemSOList;
    private DestroyedEvent destroyedEvent;

    private void Awake()
    {
        // Load components
        destroyedEvent = GetComponent<DestroyedEvent>();
    }
    private void OnEnable()
    {
        //Subscribe to destroyed event
        destroyedEvent.OnDestroyed += DestroyedEvent_OnDestroyed;
    }
    private void OnDisable()
    {
        //Unsubscribe to destroyed event
        destroyedEvent.OnDestroyed -= DestroyedEvent_OnDestroyed;
    }
    private void DestroyedEvent_OnDestroyed(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
    {
        if (!destroyedEventArgs.playerDied)
        {
            DropItemOnDeath();
        }
    }
    private void DropItemOnDeath()
    {
        int randomIndex = Random.Range(0, itemSOList.Count);
        ItemSO selectedItem = itemSOList[randomIndex];
        if (Random.Range(0, 100) < selectedItem.dropRate)
        {
            GameObject itemGO = Instantiate(selectedItem.itemPrefabs, transform.position, Quaternion.identity);
            itemGO.GetComponent<Item>().itemSO = selectedItem;
        }
    }

}

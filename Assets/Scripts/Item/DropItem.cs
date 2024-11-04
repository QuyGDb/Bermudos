using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private ItemSO item;
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
        if (Random.Range(0, 100) < item.dropRate)
        {
            Instantiate(item.itemPrefabs, transform.position, Quaternion.identity);
        }
    }
}

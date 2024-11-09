using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class ItemConsumer : MonoBehaviour
{
    InventoryManager inventoryManager;
    private DealDamage dealDamage;
    private Health health;
    private Rage rage;
    private Coroutine attackPotionCoroutine;
    private float attackPotionMultiplier = 1.25f;
    private int bigHealthPotionAmount = 50;
    private int smallHealthPotionAmount = 20;
    private int ragePotionAmount = 2;
    private InventoryItem currentInventoryItem;
    int normalDamage;
    private void Awake()
    {
        inventoryManager = GetComponent<InventoryManager>();
        dealDamage = GetComponentInChildren<DealDamage>();
        health = GetComponent<Health>();
        rage = GetComponent<Rage>();

    }
    private void OnEnable()
    {
        StaticEventHandler.OnHotBarScrollChanged += StaticEventHandler_OnHotBarScrollChanged;
    }
    private void OnDisable()
    {
        StaticEventHandler.OnHotBarScrollChanged -= StaticEventHandler_OnHotBarScrollChanged;
    }
    private void Start()
    {
        normalDamage = dealDamage.damage;
    }
    private void StaticEventHandler_OnHotBarScrollChanged(OnInventoryItemChangedEventArgs onInventoryItemChangedEventArgs)
    {
        currentInventoryItem = onInventoryItemChangedEventArgs.inventoryItem;
    }

    public void UseItemFromHotBar()
    {

        if (currentInventoryItem == null && inventoryManager.HotBarItem.Count > 0)
        {
            currentInventoryItem = inventoryManager.HotBarItem[0];
        }
        if (currentInventoryItem == null)
            return;
        foreach (InventoryItem inventoryItem in inventoryManager.inventoryItemList)
        {
            if (inventoryItem.itemSO == currentInventoryItem.itemSO)
            {
                inventoryItem.quantity--;
                Debug.Log(inventoryItem.quantity);
                UsePotion(inventoryItem.itemSO.itemName);
                StaticEventHandler.CallItemChangedEvent(inventoryItem);
                if (inventoryItem.quantity == 0)
                {
                    inventoryManager.inventoryItemList.Remove(inventoryItem);
                }
                break;
            }
        }
    }
    void UsePotion(string potionName)
    {
        switch (potionName)
        {
            case "Attack Potion":
                UseAttackPotion();
                break;
            case "Big Hp Potion":
                UseBigHealthPotion();
                break;
            case "Small Hp Potion":
                UseSmallHealthPotion();
                break;
            case "Rage Potion":
                UseRagePotion();
                break;
        }
    }
    private void UseAttackPotion()
    {
        if (attackPotionCoroutine != null)
            StopCoroutine(attackPotionCoroutine);
        dealDamage.damage = normalDamage;
        attackPotionCoroutine = StartCoroutine(UseAttackPotionCoroutine());
    }
    private IEnumerator UseAttackPotionCoroutine()
    {

        dealDamage.damage = (int)(dealDamage.damage * attackPotionMultiplier);
        yield return new WaitForSeconds(20);
        dealDamage.damage = normalDamage;
    }

    private void UseBigHealthPotion()
    {
        health.IncreaseHealth(bigHealthPotionAmount);
    }

    public void UseSmallHealthPotion()
    {
        health.IncreaseHealth(smallHealthPotionAmount);
    }
    public void UseRagePotion()
    {
        rage.IncreaseRage(ragePotionAmount);
    }

}

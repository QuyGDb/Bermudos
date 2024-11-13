using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class StaticEventHandler
{
    // Room changed event
    public static event Action<MapChangedEventArgs> OnMapChanged;

    public static void CallMapChangedEvent(Map map = default)
    {
        OnMapChanged?.Invoke(new MapChangedEventArgs() { map = map });
    }
    public static event Action OnMapTransition;
    public static void CallMapTransitionEvent()
    {
        OnMapTransition?.Invoke();
    }
    public static event Action OnNoteOpened;
    public static void CallNoteOpenedEvent()
    {
        OnNoteOpened?.Invoke();
    }
    public static event Action<OnPlayerChangedEventArgs> OnPlayerChanged;

    public static void CallPlayerChangedEvent(Player player)
    {
        OnPlayerChanged?.Invoke(new OnPlayerChangedEventArgs() { player = player });
    }

    public static event Action<OnBossChangedEventArgs> OnBossChanged;

    public static void CallBossChangedEvent(Boss boss)
    {
        OnBossChanged?.Invoke(new OnBossChangedEventArgs() { boss = boss });
    }
    public static event Action<OnBuildNavMeshEventArgs> OnBuildNavMesh;

    public static void CallBuildNavMeshEvent(bool isBuildWhenMapChanged)
    {
        OnBuildNavMesh?.Invoke(new OnBuildNavMeshEventArgs() { isBuildWhenMapChanged = isBuildWhenMapChanged });
    }
    public static event Action OnTriggerBash;
    public static void CallTriggerBashEvent()
    {
        OnTriggerBash?.Invoke();
    }

    public static event Action OnTriggerDash;

    public static void CallTriggerDashEvent()
    {
        OnTriggerDash?.Invoke();
    }
    public static event Action<OnInventoryItemChangedEventArgs> OnItemChanged;

    public static void CallItemChangedEvent(InventoryItem inventoryItem)
    {
        OnItemChanged?.Invoke(new OnInventoryItemChangedEventArgs() { inventoryItem = inventoryItem });
    }

    #region Rest Event
    public static event Action OnPressRestEvent;
    public static void CallPressRestEvent()
    {
        OnPressRestEvent?.Invoke();
    }

    public static event Action OnRestInBonfire;
    public static void CallRestInBonfireEvent()
    {
        OnRestInBonfire?.Invoke();
    }
    #endregion

    #region UI Events

    public static event Action<OnInventoryChangedEventArgs> OnInventoryChanged;

    public static void CallInventoryChangedEvent(Inventory inventory)
    {
        OnInventoryChanged?.Invoke(new OnInventoryChangedEventArgs() { inventory = inventory });
    }
    public static event Action<OnItemUIChangedEventArgs> OnItemUIEndDragChanged;

    public static void CallItemUIEndDragChangedEvent(ItemUI itemUI)
    {
        OnItemUIEndDragChanged?.Invoke(new OnItemUIChangedEventArgs() { itemUI = itemUI });
    }
    public static event Action<OnItemUIChangedEventArgs> OnItemUIHoverChanged;
    public static void CallItemUIHoverChangedEvent(ItemUI itemUI)
    {
        OnItemUIHoverChanged?.Invoke(new OnItemUIChangedEventArgs() { itemUI = itemUI });
    }
    public static event Action<OnItemUIChangedEventArgs> OnItemUIClickChanged;
    public static void CallItemUIClickChangedEvent(ItemUI itemUI)
    {
        OnItemUIClickChanged?.Invoke(new OnItemUIChangedEventArgs() { itemUI = itemUI });
    }
    public static event Action<OnInventoryItemChangedEventArgs> OnHotBarScrollChanged;
    public static void CallHotBarScrollChangedEvent(InventoryItem inventoryItem)
    {
        OnHotBarScrollChanged?.Invoke(new OnInventoryItemChangedEventArgs() { inventoryItem = inventoryItem });
    }
    public static event Action<OnInventoryItemChangedEventArgs> OnMoveItemToHotBar;
    public static void CallMoveItemToHotBarEvent(InventoryItem inventoryItem)
    {
        OnMoveItemToHotBar?.Invoke(new OnInventoryItemChangedEventArgs() { inventoryItem = inventoryItem });
    }
    #endregion

}
public class MapChangedEventArgs : EventArgs
{
    public Map map;
}

public class OnPlayerChangedEventArgs : EventArgs
{
    public Player player;
}
public class OnBossChangedEventArgs : EventArgs
{
    public Boss boss;
}
public class OnAmmoChangedEventArgs : EventArgs
{
    public Ammo ammo;
}
public class OnInventoryChangedEventArgs : EventArgs
{
    public Inventory inventory;
}
public class OnItemUIChangedEventArgs : EventArgs
{
    public ItemUI itemUI;
}
public class OnInventoryItemChangedEventArgs : EventArgs
{
    public InventoryItem inventoryItem;
}
public class OnBuildNavMeshEventArgs : EventArgs
{
    public bool isBuildWhenMapChanged;
}


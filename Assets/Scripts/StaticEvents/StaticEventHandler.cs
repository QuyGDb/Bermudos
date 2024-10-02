using System;

public static class StaticEventHandler
{
    // Room changed event
    public static event Action<MapChangedEventArgs> OnMapChanged;

    public static void CallRoomChangedEvent(Map map)
    {
        OnMapChanged?.Invoke(new MapChangedEventArgs() { map = map });
    }

    public static event Action<OnPlayerChangedEventArgs> OnPlayerChanged;

    public static void CallPlayerChangedEvent(Player player)
    {
        OnPlayerChanged?.Invoke(new OnPlayerChangedEventArgs() { player = player });
    }

    public static event Action<OnAmmoChangedEventArgs> OnAmmoChanged;

    public static void CallAmmoChangedEvent(Ammo ammo)
    {
        OnAmmoChanged?.Invoke(new OnAmmoChangedEventArgs() { ammo = ammo });
    }

}
public class MapChangedEventArgs : EventArgs
{
    public Map map;
}

public class OnPlayerChangedEventArgs : EventArgs
{
    public Player player;
}
public class OnAmmoChangedEventArgs : EventArgs
{
    public Ammo ammo;
}

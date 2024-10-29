using System;
using System.Diagnostics;

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

    public static event Action<OnBossChangedEventArgs> OnBossChanged;

    public static void CallBossChangedEvent(Boss boss)
    {
        OnBossChanged?.Invoke(new OnBossChangedEventArgs() { boss = boss });
    }
    public static event Action OnBuildNavMesh;

    public static void CallBuildNavMeshEvent()
    {
        OnBuildNavMesh?.Invoke();

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
public class OnBossChangedEventArgs : EventArgs
{
    public Boss boss;
}
public class OnAmmoChangedEventArgs : EventArgs
{
    public Ammo ammo;
}

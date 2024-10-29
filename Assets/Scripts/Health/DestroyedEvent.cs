using System;
using UnityEngine;

[DisallowMultipleComponent]
public class DestroyedEvent : MonoBehaviour
{
    public event Action<DestroyedEvent, DestroyedEventArgs> OnDestroyed;

    public void CallDestroyedEvent(DestroyedEventArgs destroyedEventArgs)
    {
        OnDestroyed?.Invoke(this, destroyedEventArgs);
    }
}

public class DestroyedEventArgs : EventArgs
{
    public bool playerDied;
    public bool bossDied;
}

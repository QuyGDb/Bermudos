using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestroyedEvent))]
[DisallowMultipleComponent]
public class Destroyed : MonoBehaviour
{
    private DestroyedEvent destroyedEvent;
    private Player player;
    private void Awake()
    {
        // Load components
        destroyedEvent = GetComponent<DestroyedEvent>();
        player = GetComponent<Player>();
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
        if (destroyedEventArgs.playerDied)
        {
            StartCoroutine(Die());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Die()
    {

        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;
    private Cinemachine.CinemachineConfiner2D cinemachineConfiner2D;
    private Collider2D mapCollider;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cinemachineConfiner2D = GetComponent<Cinemachine.CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        StaticEventHandler.OnMapChanged += StaticEventHandler_OnMapChanged;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnMapChanged -= StaticEventHandler_OnMapChanged;
    }

    private void StaticEventHandler_OnMapChanged(MapChangedEventArgs mapChangedEventArgs)
    {
        Confine(mapChangedEventArgs.map);
    }
    private void Start()
    {
        cinemachineVirtualCamera.Follow = GameManager.Instance.player.transform;

    }
    private void Confine(Map map)
    {
        if (map == null)
            return;
        mapCollider = map.GetComponent<Collider2D>();

        if (mapCollider != null)
        {
            cinemachineConfiner2D.m_BoundingShape2D = mapCollider;
        }
    }
}

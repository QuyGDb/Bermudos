using UnityEngine;
using NavMeshPlus.Components;
using System.Collections;

[DisallowMultipleComponent]
public class NavmeshManager : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    private void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();

    }
    private void OnEnable()
    {
        StaticEventHandler.OnMapChanged += StaticEventHandler_OnMapChanged;
        StaticEventHandler.OnRestInBonfire += StaticEventHandler_OnRestInBonfire;
    }

    private void OnDisable()
    {
        StaticEventHandler.OnMapChanged -= StaticEventHandler_OnMapChanged;
        StaticEventHandler.OnRestInBonfire -= StaticEventHandler_OnRestInBonfire;
    }
    private void StaticEventHandler_OnMapChanged(MapChangedEventArgs mapChangedEventArgs)
    {
        StartCoroutine(BuildNavMeshWhenMapChangedCoroutine());
    }
    private IEnumerator BuildNavMeshWhenMapChangedCoroutine()
    {
        yield return null;
        navMeshSurface.BuildNavMesh();
        StaticEventHandler.CallBuildNavMeshEvent(true);
    }
    private void StaticEventHandler_OnRestInBonfire()
    {
        StartCoroutine(BuildNavMeshWhenRestCoroutine());
    }
    private IEnumerator BuildNavMeshWhenRestCoroutine()
    {
        yield return null;
        navMeshSurface.BuildNavMesh();
        StaticEventHandler.CallBuildNavMeshEvent(false);
    }
}

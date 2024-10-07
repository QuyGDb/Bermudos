using UnityEngine;
using NavMeshPlus.Components;
using System.Collections;
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
    }
    private void OnDisable()
    {
        StaticEventHandler.OnMapChanged -= StaticEventHandler_OnMapChanged;
    }
    private void StaticEventHandler_OnMapChanged(MapChangedEventArgs mapChangedEventArgs)
    {
        StartCoroutine(BuildNavMeshCoroutine());
    }
    private IEnumerator BuildNavMeshCoroutine()
    {
        yield return null;
        navMeshSurface.BuildNavMesh();
        StaticEventHandler.CallBuildNavMeshEvent();
    }
}

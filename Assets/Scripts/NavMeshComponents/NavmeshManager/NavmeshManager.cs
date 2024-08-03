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
    private void Start()
    {
        StartCoroutine(BuildNavMeshCoroutine());
    }

    private IEnumerator BuildNavMeshCoroutine()
    {
        yield return null;
        navMeshSurface.BuildNavMesh();
    }
}

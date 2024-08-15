using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public Vector2Int templateLowerBounds;
    public Vector2Int templateUpperBounds;
    [SerializeField] private Tilemap collisionTilemap;
    [HideInInspector] public Grid grid;
    [HideInInspector] public int[,] aStarMovementPenalty;
    private void Awake()
    {
        grid = GetComponentInChildren<Grid>();
    }

    private void OnEnable()
    {
        StaticEventHandler.CallRoomChangedEvent(this);
    }


}

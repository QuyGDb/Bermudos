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

    private void OnDisable()
    {
        StaticEventHandler.CallRoomChangedEvent(null);
    }
    private void Start()
    {
        AddObstaclesAndPreferredPaths();
    }

    /// <summary>
    /// Update obstacles used by AStar pathfinmding.
    /// </summary>
    private void AddObstaclesAndPreferredPaths()
    {
        // this array will be populated with wall obstacles 
        aStarMovementPenalty = new int[templateUpperBounds.x - templateLowerBounds.x + 1, templateUpperBounds.y - templateLowerBounds.y + 1];


        // Loop thorugh all grid squares
        for (int x = 0; x < (templateUpperBounds.x - templateLowerBounds.x + 1); x++)
        {
            for (int y = 0; y < (templateUpperBounds.y - templateLowerBounds.y + 1); y++)
            {
                // Set default movement penalty for grid sqaures
                aStarMovementPenalty[x, y] = Settings.defaultAStarMovementPenalty;

                // Add obstacles for collision tiles the enemy can't walk on
                TileBase tile = collisionTilemap.GetTile(new Vector3Int(x + templateLowerBounds.x, y + templateLowerBounds.y, 0));

                foreach (TileBase collisionTile in GameResources.Instance.enemyUnwalkableCollisionTilesArray)
                {
                    if (tile == collisionTile)
                    {
                        aStarMovementPenalty[x, y] = 0;
                        break;
                    }
                }

                // Add preferred path for enemies (1 is the preferred path value, default value for
                // a grid location is specified in the Settings).
                if (tile == GameResources.Instance.preferredEnemyPathTile)
                {
                    aStarMovementPenalty[x, y] = Settings.preferredPathAStarMovementPenalty;
                }

            }
        }

    }
}

using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider2D;
    private List<Vector2> spritePhysicsShapePointsList = new List<Vector2>();
    private void Awake()
    {
        // Load components
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();

    }

    private void Start()
    {
        polygonCollider2D.useDelaunayMesh = true;
        polygonCollider2D.isTrigger = true;
    }
    private void Update()
    {
        GetWeaponCollider();
    }

    private void GetWeaponCollider()
    {
        spriteRenderer.sprite.GetPhysicsShape(0, spritePhysicsShapePointsList);

        polygonCollider2D.points = spritePhysicsShapePointsList.ToArray();
    }
}

using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Weapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [HideInInspector] public PolygonCollider2D polygonCollider2D;
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

    public void GetWeaponCollider()
    {
        int shapeCount = spriteRenderer.sprite.GetPhysicsShape(0, spritePhysicsShapePointsList);
        if (shapeCount > 0)
            polygonCollider2D.points = spritePhysicsShapePointsList.ToArray();
    }
}

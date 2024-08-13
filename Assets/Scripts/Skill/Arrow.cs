using UnityEngine;

public class Arrow : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void MakeArrow(Vector2 startPosition, Vector2 endPosition)
    {
        Vector2 directionArrow = (endPosition - startPosition);
        Vector2 perpendicularVector = new Vector2(-directionArrow.y, directionArrow.x).normalized;
        Vector2 arrowHeadPoint = endPosition + directionArrow;
        Vector2 tailPoint1 = arrowHeadPoint - directionArrow.normalized + perpendicularVector;
        Vector2 tailPoint2 = arrowHeadPoint - directionArrow.normalized - perpendicularVector;
        lineRenderer.positionCount = 5;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, arrowHeadPoint);
        lineRenderer.SetPosition(2, tailPoint1);
        lineRenderer.SetPosition(3, arrowHeadPoint);
        lineRenderer.SetPosition(4, tailPoint2);

    }

    public void ClearArrow()
    {

        lineRenderer.positionCount = 0;

    }
}


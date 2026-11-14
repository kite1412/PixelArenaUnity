using UnityEngine;

public class WorldBoundsController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D cameraBounds;
    [SerializeField] private EdgeCollider2D worldBounds;

    public void UpdateBounds()
    {
        Bounds bounds = cameraBounds.bounds;
        Transform t = worldBounds.transform;

        Vector2 bottomLeft = t.InverseTransformPoint(
            new Vector3(bounds.min.x, bounds.min.y)
        );
        Vector2 bottomRight = t.InverseTransformPoint(
            new Vector3(bounds.max.x, bounds.min.y)
        );
        Vector2 topRight = t.InverseTransformPoint(
            new Vector3(bounds.max.x, bounds.max.y)
        );
        Vector2 topLeft = t.InverseTransformPoint(
            new Vector3(bounds.min.x, bounds.max.y)
        );

        worldBounds.points = new Vector2[]
        {
            bottomLeft,
            bottomRight,
            topRight,
            topLeft,
            bottomLeft
        };
    }
}

using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraBoundsController : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private BoxCollider2D boundsCollider;
    [SerializeField] private CinemachineConfiner2D confiner;

    public void UpdateBounds()
    {
        tilemap.CompressBounds();

        Bounds bounds = tilemap.localBounds;
        Vector3 worldCenter = tilemap.transform.TransformPoint(
            bounds.center
        );
        Vector3 worldSize = tilemap.transform.TransformVector(
            bounds.size
        );

        Vector3 localCenter =
            boundsCollider.transform.InverseTransformPoint(worldCenter);

        Vector3 localSize =
            boundsCollider.transform.InverseTransformVector(worldSize);

        boundsCollider.offset = new Vector2(
            localCenter.x,
            localCenter.y
        );

        boundsCollider.size = new Vector2(
            Mathf.Abs(localSize.x),
            Mathf.Abs(localSize.y)
        );

        confiner.InvalidateBoundingShapeCache();
    }
}

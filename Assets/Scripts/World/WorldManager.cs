using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private JsonTilemapLoader[] mapLoaders;

    [Header("Bounds")]
    [SerializeField] private CameraBoundsController cameraBoundsController;
    [SerializeField] private WorldBoundsController worldBoundsController;

    void Start()
    {
        RenderTilemaps();
    }

    private void RenderTilemaps()
    {
        if (mapLoaders.Length > 0)
            for (int x = 0; x < mapLoaders.Length; x++)
                mapLoaders[x].GenerateMap();

        cameraBoundsController.UpdateBounds();
        worldBoundsController.UpdateBounds();
    }
}

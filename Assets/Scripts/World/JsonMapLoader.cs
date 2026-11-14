using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class MapData
{
    public int id_offset;
    public int width;
    public int height;
    public int[] tiles;

    public int IdOffset => id_offset;
}

public class JsonMapLoader : MonoBehaviour
{
    [Header("Map Json")]
    [SerializeField] private TextAsset mapJson;
    [SerializeField] private Sprite[] tileSprites;

    [Header("Tilemap")]
    [SerializeField] private Tilemap tilemap;

    [Header("Bounds")]
    [SerializeField] private CameraBoundsController cameraBoundsController;
    [SerializeField] private WorldBoundsController worldBoundsController;

    void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        MapData mapData = JsonUtility.FromJson<MapData>(mapJson.text);

        tilemap.ClearAllTiles();

        for (int y = 0; y < mapData.height; y++)
        {
            for (int x = 0; x < mapData.width; x++)
            {
                int tileIndex = mapData.tiles[y * mapData.width + x] - mapData.IdOffset;

                if (tileIndex >= 0 && tileIndex < tileSprites.Length)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = tileSprites[tileIndex];
                    tilemap.SetTile(new Vector3Int(x, -y, 0), tile);
                }
            }
        }
        
        cameraBoundsController.UpdateBounds();
        worldBoundsController.UpdateBounds();
    }
}

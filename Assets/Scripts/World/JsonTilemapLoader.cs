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

[Serializable]
public class TilesetMetadata
{
    public int tile_size;
    public int total_column;
    public int total_row;
    public TileMetadata[] tiles;

    public int TileSize => tile_size;
    public int TotalColumn => total_column;
    public int TotalRow => total_row;
}

[Serializable]
public class TileMetadata
{
    public int id;
    public bool is_solid;
    public int base_tile_id;

    public bool IsSolid => is_solid;
    public int BaseTileId => base_tile_id;
}

public class JsonTilemapLoader : MonoBehaviour
{
    [Header("Map Json")]
    [SerializeField] private TextAsset mapJson;
    [SerializeField] private Sprite[] tileSprites;

    [Header("Tilemap")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap backgroundTilemap;

    [Header("Tileset Metadata")]
    [SerializeField] private TextAsset tilesetMetadataJson;

    void Awake()
    {
        tilemap.GetComponent<TilemapRenderer>().sortingOrder = 0;

        if (backgroundTilemap != null)
            backgroundTilemap.GetComponent<TilemapRenderer>().sortingOrder = -1;
    }

    public void GenerateMap()
    {
        MapData mapData = JsonUtility.FromJson<MapData>(mapJson.text);
        TilesetMetadata tilesetMetadata = null;

        tilemap.ClearAllTiles();
        
        OnMetadataAvailable(() =>
        {
            tilesetMetadata = JsonUtility.FromJson<TilesetMetadata>(tilesetMetadataJson.text);
            backgroundTilemap.ClearAllTiles(); 
        });

        for (int y = 0; y < mapData.height; y++)
        {
            for (int x = 0; x < mapData.width; x++)
            {
                int tileIndex = mapData.tiles[y * mapData.width + x] - mapData.IdOffset;

                if (tileIndex >= 0 && tileIndex < tileSprites.Length)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = tileSprites[tileIndex];

                    OnMetadataAvailable(() =>
                    {
                        TileMetadata tileMetadata = Array.Find(tilesetMetadata.tiles, t => t.id == tileIndex);
                        if (tileMetadata != null && tileMetadata.BaseTileId >= 0 && tileMetadata.BaseTileId < tileSprites.Length)
                        {
                            Tile baseTile = ScriptableObject.CreateInstance<Tile>();
                            baseTile.sprite = tileSprites[tileMetadata.BaseTileId];
                            backgroundTilemap.SetTile(new Vector3Int(x, -y, 0), baseTile);
                        }
                    });

                    tilemap.SetTile(new Vector3Int(x, -y, 0), tile);
                }
            }
        }
    }

    private void OnMetadataAvailable(Action action)
    {
        if (tilesetMetadataJson != null && backgroundTilemap != null)
            action.Invoke();
    }
}

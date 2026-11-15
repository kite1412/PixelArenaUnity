using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class JsonTilesetMetadataLoader
{
    [SerializeField] private TextAsset tilesetMetadataJson;
    [SerializeField] private Tilemap AdditionalTilemap;

    public void ApplyMetadata()
    {
        
    }
}

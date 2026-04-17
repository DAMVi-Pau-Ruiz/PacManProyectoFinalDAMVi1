using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BolasSpawner : MonoBehaviour
{
    [SerializeField]
    Tilemap bolasPetit;

    [SerializeField]
    GameObject bolaPetitPrefab;

    private void Start()
    {
        Respawn();
        bolasPetit.GetComponent<TilemapRenderer>().enabled = false;
        //bolasPetit.ClearAllTiles();
    }

    void SpawnBolasFromTilemap(Tilemap tilemap, GameObject prefab)
    {
        foreach(var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int cellPos = new Vector3Int(pos.x, pos.y, pos.z);
            TileBase tile = tilemap.GetTile(cellPos);

            if (tile == null)
            {
                continue;
            }
            Vector3 worldPos = tilemap.CellToWorld(cellPos) + new Vector3(0.5f, 0.5f, 0);

            Instantiate(prefab, worldPos, Quaternion.identity);
        }
    }

    public void Respawn()
    {
        BolasManager.Instance.ResetCounter();
        SpawnBolasFromTilemap(bolasPetit, bolaPetitPrefab);
    }

}

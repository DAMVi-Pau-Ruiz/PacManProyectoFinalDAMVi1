using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BolasSpawner : MonoBehaviour
{
    [SerializeField] Tilemap bolasPetit;
    [SerializeField] Tilemap bolasGrandes;

    [SerializeField] GameObject bolaPetitPrefab;
    [SerializeField] GameObject bolaGrandePrefab;

    private void Start()
    {
        Respawn();

        // Ocultar solo los renderers, NO borrar los tiles
        bolasPetit.GetComponent<TilemapRenderer>().enabled = false;
        bolasGrandes.GetComponent<TilemapRenderer>().enabled = false;
    }

    void SpawnBolasFromTilemap(Tilemap tilemap, GameObject prefab)
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int cellPos = new Vector3Int(pos.x, pos.y, pos.z);
            TileBase tile = tilemap.GetTile(cellPos);

            if (tile == null)
                continue;

            Vector3 worldPos = tilemap.CellToWorld(cellPos) + new Vector3(0.5f, 0.5f, 0);

            Instantiate(prefab, worldPos, Quaternion.identity);
        }
    }

    public void Respawn()
    {
        BolasManager.Instance.ResetCounter();

        SpawnBolasFromTilemap(bolasPetit, bolaPetitPrefab);
        SpawnBolasFromTilemap(bolasGrandes, bolaGrandePrefab);
    }
}

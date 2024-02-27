using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class test2 : MonoBehaviour
{
    // Start is called before the first frame update
   
    
         public Tilemap tilemap;
    public GameObject tilePrefab;

    void Start()
    {
        Vector3Int min = tilemap.cellBounds.min;
        Vector3Int max = tilemap.cellBounds.max;

        for (int x = min.x; x < max.x; x++)
        {
            for (int y = min.y; y < max.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePosition);

                if (tile != null)
                {
                    Vector3 tileWorldPos = tilemap.CellToWorld(tilePosition);
                    Instantiate(tilePrefab, tileWorldPos, Quaternion.identity);
                }
            }
        }
    }
}

 
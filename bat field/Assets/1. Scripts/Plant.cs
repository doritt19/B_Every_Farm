using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            test.gridX = 7;
            test.gridY = 2;

            // 타일의 왼쪽 하단 모서리 위치를 가져옵니다.
            Vector3Int cellPosition = new Vector3Int((int)test.gridX, (int)test.gridY, 0);
            Vector3 worldPosition = test.tilemap.CellToWorld(cellPosition);

            // 타일의 크기를 고려하여 오브젝트의 위치를 조정합니다.
            worldPosition.x += test.tileSizeX / 2f;
            worldPosition.y += test.tileSizeY / 2f;

            // 오브젝트를 생성하고 해당 위치로 이동합니다.
            GameObject newObject = Instantiate(gameObject, worldPosition, Quaternion.identity);
            Debug.Log("New object position: " + newObject.transform.position);
        }
    }
}

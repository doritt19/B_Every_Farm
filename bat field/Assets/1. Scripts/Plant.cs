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

            // Ÿ���� ���� �ϴ� �𼭸� ��ġ�� �����ɴϴ�.
            Vector3Int cellPosition = new Vector3Int((int)test.gridX, (int)test.gridY, 0);
            Vector3 worldPosition = test.tilemap.CellToWorld(cellPosition);

            // Ÿ���� ũ�⸦ ����Ͽ� ������Ʈ�� ��ġ�� �����մϴ�.
            worldPosition.x += test.tileSizeX / 2f;
            worldPosition.y += test.tileSizeY / 2f;

            // ������Ʈ�� �����ϰ� �ش� ��ġ�� �̵��մϴ�.
            GameObject newObject = Instantiate(gameObject, worldPosition, Quaternion.identity);
            Debug.Log("New object position: " + newObject.transform.position);
        }
    }
}

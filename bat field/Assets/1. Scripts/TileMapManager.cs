using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    
    // 5x5 ũ���� Ÿ�ϸ��� ��Ÿ���� ������ �迭
    private GameObject[,] tiles = new GameObject[5, 5];

    private void Awake()
    {
        
    }
    // Ÿ�ϸ� �ʱ�ȭ
    private void InitializeTileMap()
    {
        // �̹� ������ Ÿ�ϸ� ��������
        Tilemap tilemap = GetComponent<Tilemap>();

        // Ÿ�ϸ��� �� Ÿ�Ͽ� ���� GameObject ����
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3Int tilePosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(tilePosition);
                // Ÿ�ϸʿ��� Ÿ���� ������ �� �ش� Ÿ���� �����ϴ� ���
                if (tile != null)
                {
                    // �ش� ��ġ�� Ÿ�Ͽ� ���� GameObject ��������
                    GameObject tileObject = tilemap.GetInstantiatedObject(tilePosition);
                    // Ÿ�ϸ� �迭�� ����
                    tiles[i, j] = tileObject;
                }
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) // ���콺 ���� ��ư Ŭ�� ��
        {
            // ���콺 Ŭ���� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ����ĳ��Ʈ �浹�� �˻��� ���̾� ����ũ ����
            int layerMask = LayerMask.GetMask("Field"); // "Field" ���̾ ���� ����ũ ����

            // Ŭ���� ��ġ���� ����ĳ��Ʈ �߻��Ͽ� �浹�� ��ü Ȯ��
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity, layerMask);

            // �浹�� ��ü�� �ִ� ���
            if (hit.collider != null)
            {
                // �浹�� ��ġ�� Ÿ�� ��ǥ�� ������
                int tileX = Mathf.FloorToInt(clickPosition.x);
                int tileY = Mathf.FloorToInt(clickPosition.y);

                // Ÿ���� ��ǥ�� (1,1)���� (8,8)������ ���� ���� �ִ��� Ȯ��
                if (tileX >= 1 && tileX <= 8 && tileY >= 1 && tileY <= 8)
                {
                    // Ŭ���� ��ġ�� �ش��ϴ� Ÿ���� ��ǥ�� ���
                    Debug.Log("Clicked tile position: (" + tileX + ", " + tileY + ")");
                }
                else
                {
                    // ������ ��� ��� �޽��� ���
                    Debug.Log("Clicked position is outside of tilemap bounds.");
                }
            }
        }
    }
    // Ư�� ��ġ�� Ÿ�Ͽ� ������Ʈ �߰�
    public void AddObjectToTile(int x, int y, GameObject obj)
    {
        // �ش� ��ġ�� ������Ʈ �߰�
        obj.transform.position = new Vector3(x, y, 0);
        // Ÿ�ϸ� �迭�� ������Ʈ �߰�
        tiles[x, y] = obj;
    }

    // Ư�� ��ġ�� Ÿ�Ͽ��� ������Ʈ ����
    public void RemoveObjectFromTile(int x, int y)
    {
        // �ش� ��ġ�� ������Ʈ ����
        Destroy(tiles[x, y]);
        // Ÿ�ϸ� �迭���� ������Ʈ ����
        tiles[x, y] = null;
    }
}

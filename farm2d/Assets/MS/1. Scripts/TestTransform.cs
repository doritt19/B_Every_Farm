using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TestTransform : MonoBehaviour
{
    public Tilemap tilemap; // Ŭ���� Ÿ�ϸ��� ������ ����
    //public GameObject inven;
    private GameObject previousInven;
    public GameObject[] seed;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) // ���콺 ���� ��ư Ŭ�� ��
        {
            // Ŭ���� ��ġ�� Ÿ�ϸ��� ã�Ƽ� �Ҵ�
            FindClickedTilemap();
            if (tilemap != null)
            {
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = tilemap.WorldToCell(clickPosition);
                Vector3Int tilePosition = new Vector3Int(cellPosition.x, cellPosition.y, 0);

                if (tilemap.HasTile(tilePosition)) // Ŭ���� ��ġ�� Ÿ���� �ִ��� Ȯ��
                {
                    Vector3 tileCenter = CalculateTileCenter(cellPosition, tilemap);
                    Debug.Log("Clicked Tile Center: " + tileCenter);
                    // ������ ������ �κ��丮 ������Ʈ�� ������ ����
                    if (previousInven != null)
                    {
                        Destroy(previousInven);
                    }

                    // ���ο� �κ��丮 ������Ʈ ����
                    Vector3 instant = new Vector3(tileCenter.x + 2, tileCenter.y + 2.5f, tileCenter.z + 10);
                    //previousInven = Instantiate(inven, instant, Quaternion.identity);
                }

            }
        }
    }

    void FindClickedTilemap()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
        if (hit.collider != null)
        {
            // Ŭ���� ��ġ���� �浹�� Collider�� �ִ� ���
            GameObject hitObject = hit.collider.gameObject;
            Tilemap hitTilemap = hitObject.GetComponent<Tilemap>();
            if (hitTilemap != null && hitObject.layer == LayerMask.NameToLayer("Field"))
            {
                // �ʵ� ���̾ ���� Ÿ�ϸ��� ���
                tilemap = hitTilemap;
            }
        }
    }

    Vector3 CalculateTileCenter(Vector3Int cellPosition, Tilemap tilemap)
    {
        Vector3 tileCenter = tilemap.GetCellCenterWorld(cellPosition);
        // Ÿ���� �߽� ��ǥ ���
        tileCenter.x += 0;
        tileCenter.y += 0.5f;
        return tileCenter;
    }
}

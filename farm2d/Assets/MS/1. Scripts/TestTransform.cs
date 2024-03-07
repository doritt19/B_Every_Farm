using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TestTransform : MonoBehaviour
{
    public Tilemap tilemap; // 클릭한 타일맵을 저장할 변수
    //public GameObject inven;
    private GameObject previousInven;
    public GameObject[] seed;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) // 마우스 왼쪽 버튼 클릭 시
        {
            // 클릭한 위치의 타일맵을 찾아서 할당
            FindClickedTilemap();
            if (tilemap != null)
            {
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPosition = tilemap.WorldToCell(clickPosition);
                Vector3Int tilePosition = new Vector3Int(cellPosition.x, cellPosition.y, 0);

                if (tilemap.HasTile(tilePosition)) // 클릭한 위치에 타일이 있는지 확인
                {
                    Vector3 tileCenter = CalculateTileCenter(cellPosition, tilemap);
                    Debug.Log("Clicked Tile Center: " + tileCenter);
                    // 이전에 생성된 인벤토리 오브젝트가 있으면 삭제
                    if (previousInven != null)
                    {
                        Destroy(previousInven);
                    }

                    // 새로운 인벤토리 오브젝트 생성
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
            // 클릭한 위치에서 충돌한 Collider가 있는 경우
            GameObject hitObject = hit.collider.gameObject;
            Tilemap hitTilemap = hitObject.GetComponent<Tilemap>();
            if (hitTilemap != null && hitObject.layer == LayerMask.NameToLayer("Field"))
            {
                // 필드 레이어에 속한 타일맵인 경우
                tilemap = hitTilemap;
            }
        }
    }

    Vector3 CalculateTileCenter(Vector3Int cellPosition, Tilemap tilemap)
    {
        Vector3 tileCenter = tilemap.GetCellCenterWorld(cellPosition);
        // 타일의 중심 좌표 계산
        tileCenter.x += 0;
        tileCenter.y += 0.5f;
        return tileCenter;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    
    // 5x5 크기의 타일맵을 나타내는 이차원 배열
    private GameObject[,] tiles = new GameObject[5, 5];

    private void Awake()
    {
        
    }
    // 타일맵 초기화
    private void InitializeTileMap()
    {
        // 이미 생성된 타일맵 가져오기
        Tilemap tilemap = GetComponent<Tilemap>();

        // 타일맵의 각 타일에 대해 GameObject 저장
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3Int tilePosition = new Vector3Int(i, j, 0);
                TileBase tile = tilemap.GetTile(tilePosition);
                // 타일맵에서 타일을 가져올 때 해당 타일이 존재하는 경우
                if (tile != null)
                {
                    // 해당 위치의 타일에 대한 GameObject 가져오기
                    GameObject tileObject = tilemap.GetInstantiatedObject(tilePosition);
                    // 타일맵 배열에 저장
                    tiles[i, j] = tileObject;
                }
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) // 마우스 왼쪽 버튼 클릭 시
        {
            // 마우스 클릭한 위치를 월드 좌표로 변환
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 레이캐스트 충돌을 검사할 레이어 마스크 설정
            int layerMask = LayerMask.GetMask("Field"); // "Field" 레이어에 대한 마스크 설정

            // 클릭한 위치에서 레이캐스트 발사하여 충돌한 객체 확인
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity, layerMask);

            // 충돌한 객체가 있는 경우
            if (hit.collider != null)
            {
                // 충돌한 위치의 타일 좌표를 가져옴
                int tileX = Mathf.FloorToInt(clickPosition.x);
                int tileY = Mathf.FloorToInt(clickPosition.y);

                // 타일의 좌표가 (1,1)부터 (8,8)까지의 범위 내에 있는지 확인
                if (tileX >= 1 && tileX <= 8 && tileY >= 1 && tileY <= 8)
                {
                    // 클릭된 위치에 해당하는 타일의 좌표를 출력
                    Debug.Log("Clicked tile position: (" + tileX + ", " + tileY + ")");
                }
                else
                {
                    // 범위를 벗어난 경우 메시지 출력
                    Debug.Log("Clicked position is outside of tilemap bounds.");
                }
            }
        }
    }
    // 특정 위치의 타일에 오브젝트 추가
    public void AddObjectToTile(int x, int y, GameObject obj)
    {
        // 해당 위치에 오브젝트 추가
        obj.transform.position = new Vector3(x, y, 0);
        // 타일맵 배열에 오브젝트 추가
        tiles[x, y] = obj;
    }

    // 특정 위치의 타일에서 오브젝트 제거
    public void RemoveObjectFromTile(int x, int y)
    {
        // 해당 위치의 오브젝트 제거
        Destroy(tiles[x, y]);
        // 타일맵 배열에서 오브젝트 제거
        tiles[x, y] = null;
    }
}

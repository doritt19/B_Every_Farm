using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Create : MonoBehaviour
{
    public GameObject[,] tiles = new GameObject[8, 8]; // 8x8 크기의 타일맵 배열
    public float tileSize = 1.0f; // 타일의 크기

    void Start()
    {
        // 타일맵의 각 블록에 대한 정보를 초기화
        InitializeTileMap();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 마우스 클릭한 위치를 월드 좌표로 변환
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 아이소메트릭 좌표로 변환
            Vector2 isoPosition = CartesianToIso(clickPosition);

            // 해당 타일의 인덱스 계산
            int tileX = Mathf.FloorToInt(isoPosition.x / tileSize);
            int tileY = Mathf.FloorToInt(isoPosition.y / tileSize);

            // 해당 타일의 인덱스 출력
            Debug.Log("Clicked tile index: (" + tileX + ", " + tileY + ")");
        }
    }

    // 타일맵 초기화 함수
    private void InitializeTileMap()
    {
        // 타일맵의 각 타일에 대해 GameObject 저장
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                // 타일의 위치를 계산하여 해당하는 GameObject를 가져오고 배열에 저장
                GameObject tileObject = GetTileObject(x, y);
                tiles[x, y] = tileObject;
            }
        }
    }

    // 주어진 타일의 위치에 해당하는 GameObject 가져오기
    private GameObject GetTileObject(int x, int y)
    {
        // 여기서는 간단히 빈 GameObject를 생성하여 반환하는 예시를 보여줍니다.
        GameObject tileObject = new GameObject("Tile_" + x + "_" + y);
        tileObject.transform.position = IsoToCartesian(new Vector2(x, y)); // 타일의 위치 설정

        // 여기서는 간단히 빈 GameObject를 반환합니다.
        return tileObject;
    }

    // 아이소메트릭 좌표를 직사각형 좌표로 변환하는 함수
    private Vector3 IsoToCartesian(Vector2 isoPosition)
    {
        Vector3 cartPosition = Vector3.zero;
        cartPosition.x = (isoPosition.x - isoPosition.y) * (tileSize / 2);
        cartPosition.y = (isoPosition.x + isoPosition.y) * (tileSize / 4);
        return cartPosition;
    }

    // 직사각형 좌표를 아이소메트릭 좌표로 변환하는 함수
    private Vector2 CartesianToIso(Vector3 cartPosition)
    {
        Vector2 isoPosition = Vector2.zero;
        isoPosition.x = (cartPosition.x / (tileSize / 2) + cartPosition.y / (tileSize / 4)) / 2;
        isoPosition.y = (cartPosition.y / (tileSize / 4) - cartPosition.x / (tileSize / 2)) / 2;
        return isoPosition;
    }
}

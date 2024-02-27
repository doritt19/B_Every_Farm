using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class test : MonoBehaviour
{

    
    public static Tilemap tilemap; // Tilemap 컴포넌트 참조
  public static float gridX;
  public static float gridY;
    public static float tileSizeX = 1.0f; // 타일의 가로 크기
    public static float tileSizeY = 0.5f; // 타일의 세로 크기
    public GameObject testObj;
    private void Start()
    {
        
        tilemap = GetComponent<Tilemap>();
        Vector3 tileSize = tilemap.cellSize;
        Debug.Log("Tile Size: " + tileSize);

    }
    void Update()
    {
        

        { // 마우스 클릭한 위치를 월드 좌표로 변환
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 클릭한 위치를 타일맵의 로컬 좌표로 변환
            Vector3Int cellPosition = tilemap.WorldToCell(clickPosition);


            // 타일의 실제 위치 계산
            Vector3 tilePosition = tilemap.CellToWorld(cellPosition);
            tilePosition.z = 0;
            tilePosition.y += 1;
            // 타일의 그리드 좌표 계산 (1,1)부터 시작
            int gridX = cellPosition.x + 1;
            int gridY = Mathf.FloorToInt((cellPosition.y + 1) * 1); // 타일의 높이를 곱하여 정수로 변환

            if (Input.GetKeyDown(KeyCode.X))
            {
                // 타일의 실제 위치를 계산하여 해당 위치에 새로운 게임 오브젝트를 생성합니다.
                Vector3 worldPosition = new Vector3(gridX + tileSizeX / 2, gridY + tileSizeY / 2, 0);
                GameObject newObject = Instantiate(testObj, tilePosition, Quaternion.identity);

                // 생성된 게임 오브젝트의 위치를 그리드 좌표로 변환하여 디버그합니다.
                int objectGridX = Mathf.FloorToInt((worldPosition.x + tileSizeX / 2) / tileSizeX);
                int objectGridY = Mathf.FloorToInt((worldPosition.y + tileSizeY / 2) / tileSizeY);
                Debug.Log("Created object grid position: (" + objectGridX + ", " + objectGridY + ")");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Clicked tile grid position: (" + gridX + ", " + gridY + ")");
                Debug.Log("실제 위치 :" + tilePosition);
            }
        }
    }
    public void OnButtonClick1()
    {
        StartCoroutine(SpawnObjectsWithCooldown(2.5f,2f));
    }
    public void OnButtonClick2()
    {
        StartCoroutine(SpawnObjectsWithCooldown(2f, 1.75f));
    }

    public void OnButtonClick3()
    {
        StartCoroutine(SpawnObjectsWithCooldown(1.5f, 1.5f));
    }
    public void OnButtonClick4()
    {
        StartCoroutine(SpawnObjectsWithCooldown(1f, 1.25f));
    }
    public void OnButtonClick5()
    {
        StartCoroutine(SpawnObjectsWithCooldown(0.5f, 1f));
    }
    public void OnButtonClick6()
    {
        StartCoroutine(SpawnObjectsWithCooldown(0f, 0.75f));
    }
    public void OnButtonClick7()
    {
        StartCoroutine(SpawnObjectsWithCooldown(-0.5f, 0.5f));
    }
    public void OnButtonClick8()
    {
        StartCoroutine(SpawnObjectsWithCooldown(-1f, 0.25f));
    }
    IEnumerator SpawnObjectsWithCooldown(float x, float y)
    {
        
        

        for (int i = 0; i < 8; i++)
        {
            Debug.Log($"x,y = ({x},{y})");
            Vector3 newPosition = new Vector3(x, y, 0);
            Instantiate(testObj, newPosition, Quaternion.identity);

            x += 0.5f;
            y -= 0.25f;
            yield return new WaitForSeconds(0.25f);
        }


        //for (int x = -1; x <= 6; x++)
        //{
        //    for (float y = 2.0f; y >= -1.5f; y -= 0.5f)
        //    {
        //        Vector3 newPosition =  new Vector3(x, y, 0);
        //        GameObject newObject = Instantiate(testObj, newPosition, Quaternion.identity);
        //        yield return new WaitForSeconds(1.0f);
        //    }
        //}
        
    }
    void For(float x, float y)
    {
        for (int i = 0; i < 8; i++)
        {
            Debug.Log($"x,y = ({x},{y})");
            Vector3 newPosition = new Vector3(x, y, 0);
            Instantiate(testObj, newPosition, Quaternion.identity);

            x += 0.5f;
            y -= 0.25f;
           
        }
    }

}





 



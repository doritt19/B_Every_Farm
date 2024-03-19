using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryButton : MonoBehaviour
{
    public Shopbutton shopbutton; //2024-03-13 추가 상점과 인벤 연동   
    public Text[] seedText;

    public Tilemap tilemap; // 클릭한 타일맵을 저장할 변수
    public GameObject[] prefabObject;
    public GameObject[] seedPrefabs;

    private GameObject spawnedObject;

    bool isObjectSpawned = false;

    public static List<Vector3> tileCenterList = new List<Vector3>();
    int spawnIndex;

    void Start()
    {

        spawnedObject = null;

    }

    public void SpawnObject(int plant)
    {

        if (spawnedObject == null)
        {

            // 프리팹 오브젝트 복사
            spawnedObject = Instantiate(prefabObject[plant], Vector3.zero, Quaternion.identity);
            spawnIndex = plant;
            //Debug.Log("q") 테스트용 로그
            // 마우스 클릭 상태로 설정
            SetObjectToFollowMouse(true);
        }
    }


    void Update()
    {
        if (spawnedObject != null)
        {

            // 마우스 클릭시 씨앗 오브젝트 파괴
            if (Input.GetMouseButtonDown(0))
            { // 클릭한 위치의 타일맵을 찾아서 할당
                FindClickedTilemap();

                if (tilemap != null)
                {
                    Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int cellPosition = tilemap.WorldToCell(clickPosition);
                    Vector3Int tilePosition = new Vector3Int(cellPosition.x, cellPosition.y, 0);

                    if (tilemap.HasTile(tilePosition)) // 클릭한 위치에 타일이 있는지 확인
                    {


                        Vector3 tileCenter = CalculateTileCenter(cellPosition, tilemap);

                        tileCenter.z += 10;



                        int index = Mathf.Clamp(prefabObject.Length - 1, 0, seedPrefabs.Length - 1);
                        GameObject seedPrefab = seedPrefabs[spawnIndex];

                        // 겹치지 않게 심어지기 위한 작업
                        if (tileCenterList != null)
                        {



                            if (!tileCenterList.Contains(tileCenter) && shopbutton.vagetableSeed[spawnIndex] >= 1)
                            {
                                //2024-03-13 추가 상점과 인벤 연동   
                                for (int i = 0; i < seedText.Length; i++)
                                {
                                    seedText[i].text = shopbutton.vagetableSeed[i].ToString();
                                }
                                shopbutton.vagetableSeed[spawnIndex] -= 1;//2024-03-13 추가 상점과 인벤 연동   
                                seedText[spawnIndex].text = shopbutton.vagetableSeed[spawnIndex].ToString();
                                tileCenterList.Add(tileCenter);
                                Instantiate(seedPrefab, tileCenter, Quaternion.identity);
                            }

                        }
                        //OnDrawGizmos(seedPrefab);

                    }

                }

                Destroy(spawnedObject);
                spawnedObject = null;
            }
            else
            {
                // 마우스 클릭 중일 때 오브젝트를 마우스의 위치로 이동
                MoveObjectWithMouse();
            }

        }
    }

    void MoveObjectWithMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // 카메라와 오브젝트 사이의 거리 조정
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        spawnedObject.transform.position = worldPosition;
    }

    void SetObjectToFollowMouse(bool follow)
    {
        if (spawnedObject != null)
        {
            if (follow)
            {
                // 오브젝트를 마우스의 위치로 이동시키기 위해 오브젝트에 Rigidbody를 추가하고, isKinematic을 true로 설정
                Rigidbody rb = spawnedObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;
            }
            else
            {
                // 마우스 클릭 해제 시 Rigidbody 제거
                Destroy(spawnedObject.GetComponent<Rigidbody>());
            }
        }
    }
    void PlaceObjectAtMousePosition()
    {
        // 마우스의 현재 위치를 기준으로 오브젝트를 생성합니다.
        int index = Mathf.Clamp(prefabObject.Length - 1, 0, seedPrefabs.Length - 1);
        GameObject seedPrefab = seedPrefabs[index];
        if (seedPrefab != null)
        {
            Instantiate(seedPrefab, spawnedObject.transform.position, Quaternion.identity);
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
        tileCenter.y += 0.2f;
        return tileCenter;
    }
    public void SpawnObjectIndex(int a, Vector3 tileCenter)
    {
        Instantiate(seedPrefabs[a], tileCenter, Quaternion.identity);
    }

}


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
    public Shopbutton shopbutton; //2024-03-13 �߰� ������ �κ� ����   
    public Text[] seedText;

    public Tilemap tilemap; // Ŭ���� Ÿ�ϸ��� ������ ����
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

            // ������ ������Ʈ ����
            spawnedObject = Instantiate(prefabObject[plant], Vector3.zero, Quaternion.identity);
            spawnIndex = plant;
            //Debug.Log("q") �׽�Ʈ�� �α�
            // ���콺 Ŭ�� ���·� ����
            SetObjectToFollowMouse(true);
        }
    }


    void Update()
    {
        if (spawnedObject != null)
        {

            // ���콺 Ŭ���� ���� ������Ʈ �ı�
            if (Input.GetMouseButtonDown(0))
            { // Ŭ���� ��ġ�� Ÿ�ϸ��� ã�Ƽ� �Ҵ�
                FindClickedTilemap();

                if (tilemap != null)
                {
                    Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int cellPosition = tilemap.WorldToCell(clickPosition);
                    Vector3Int tilePosition = new Vector3Int(cellPosition.x, cellPosition.y, 0);

                    if (tilemap.HasTile(tilePosition)) // Ŭ���� ��ġ�� Ÿ���� �ִ��� Ȯ��
                    {


                        Vector3 tileCenter = CalculateTileCenter(cellPosition, tilemap);

                        tileCenter.z += 10;



                        int index = Mathf.Clamp(prefabObject.Length - 1, 0, seedPrefabs.Length - 1);
                        GameObject seedPrefab = seedPrefabs[spawnIndex];

                        // ��ġ�� �ʰ� �ɾ����� ���� �۾�
                        if (tileCenterList != null)
                        {



                            if (!tileCenterList.Contains(tileCenter) && shopbutton.vagetableSeed[spawnIndex] >= 1)
                            {
                                //2024-03-13 �߰� ������ �κ� ����   
                                for (int i = 0; i < seedText.Length; i++)
                                {
                                    seedText[i].text = shopbutton.vagetableSeed[i].ToString();
                                }
                                shopbutton.vagetableSeed[spawnIndex] -= 1;//2024-03-13 �߰� ������ �κ� ����   
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
                // ���콺 Ŭ�� ���� �� ������Ʈ�� ���콺�� ��ġ�� �̵�
                MoveObjectWithMouse();
            }

        }
    }

    void MoveObjectWithMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // ī�޶�� ������Ʈ ������ �Ÿ� ����
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        spawnedObject.transform.position = worldPosition;
    }

    void SetObjectToFollowMouse(bool follow)
    {
        if (spawnedObject != null)
        {
            if (follow)
            {
                // ������Ʈ�� ���콺�� ��ġ�� �̵���Ű�� ���� ������Ʈ�� Rigidbody�� �߰��ϰ�, isKinematic�� true�� ����
                Rigidbody rb = spawnedObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;
            }
            else
            {
                // ���콺 Ŭ�� ���� �� Rigidbody ����
                Destroy(spawnedObject.GetComponent<Rigidbody>());
            }
        }
    }
    void PlaceObjectAtMousePosition()
    {
        // ���콺�� ���� ��ġ�� �������� ������Ʈ�� �����մϴ�.
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
        tileCenter.y += 0.2f;
        return tileCenter;
    }
    public void SpawnObjectIndex(int a, Vector3 tileCenter)
    {
        Instantiate(seedPrefabs[a], tileCenter, Quaternion.identity);
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject[] prefabObject;

    private GameObject spawnedObject;

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
            //Debug.Log("q") 테스트용 로그
            // 마우스 클릭 상태로 설정
            SetObjectToFollowMouse(true);
        }
    }

    void Update()
    {
        if (spawnedObject != null)
        {
            
            // 마우스 클릭시 오브젝트 파괴
            if (Input.GetMouseButtonDown(0))
            {
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
}


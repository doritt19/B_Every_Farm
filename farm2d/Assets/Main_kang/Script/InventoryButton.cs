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
            
            // ������ ������Ʈ ����
            spawnedObject = Instantiate(prefabObject[plant], Vector3.zero, Quaternion.identity);
            //Debug.Log("q") �׽�Ʈ�� �α�
            // ���콺 Ŭ�� ���·� ����
            SetObjectToFollowMouse(true);
        }
    }

    void Update()
    {
        if (spawnedObject != null)
        {
            
            // ���콺 Ŭ���� ������Ʈ �ı�
            if (Input.GetMouseButtonDown(0))
            {
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
}


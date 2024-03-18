using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmEditorXX : MonoBehaviour
{
    public Vector3 OriginalPosition; // ������Ʈ�� �ʱ� ��ġ�� ������ ����
    public GameObject[] Buildings; // ������ �ǹ����� �Ҵ��ϴ� ����
    public Tilemap tilemap; // Ÿ�ϸ��� ĭ�� ���� �ǹ��� �̵��ϱ� ���� Ÿ�ϸ� ����
    public GameObject selectedObject; // ���õ� ������Ʈ�� �����ϴ� ����

    public void EditStart()
    {
        gameObject.SetActive(true);
    }

    public void EditEnd()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� Ŭ������ ��
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos); // ���콺 Ŭ�� ��ġ�� Ÿ�ϸ��� �� ��ǥ�� ��ȯ

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero); // ���콺 ��ġ���� ����ĳ��Ʈ�� �߻��Ͽ� �浹 ���� Ȯ��

            if (hit.collider != null) // ����ĳ��Ʈ�� � ������Ʈ�� �浹���� ���
            {
                GameObject clickedObject = hit.collider.gameObject; // �浹�� ������Ʈ ����
                if (IsBuilding(clickedObject)) // �浹�� ������Ʈ�� �ǹ����� Ȯ��
                {
                    selectedObject = clickedObject; // ���õ� ������Ʈ�� ����
                    // ���õ� ������Ʈ�� �ʱ� ��ġ�� �ߺ����� �ʰ� �߰�
                    OriginalPosition = selectedObject.transform.position;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0)) // ���콺 ���� ��ư�� ���� ��
        {
            if (selectedObject != null) // ���õ� ������Ʈ�� �ִ� ���
            {
                RaycastHit2D overlap = Physics2D.Raycast(selectedObject.transform.position, Vector3.zero); // ���õ� ������Ʈ ��ġ���� ����ĳ��Ʈ �߻�

                if (overlap.collider != null && overlap.collider.gameObject != selectedObject) // ���õ� ������Ʈ�� �ٸ� ������Ʈ�� ��ġ�� ���
                {
                    selectedObject.transform.position = OriginalPosition; // ���õ� ������Ʈ�� ��ġ�� �ʱ� ��ġ�� �ǵ���
                }
                selectedObject = null; // ���õ� ������Ʈ �ʱ�ȭ
            }
        }

                if (selectedObject != null) // ���õ� ������Ʈ�� �ִ� ���
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // z���� 0���� �����Ͽ� ������Ʈ�� ī�޶� ������ �������� ����
            // Ÿ�ϸ� �� ��ġ�� �°� ������Ʈ ��ġ ����
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);
            Vector3 targetPos = tilemap.GetCellCenterWorld(cellPosition);
            selectedObject.transform.position = targetPos;
        }
    }

    bool IsBuilding(GameObject obj)
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            if (obj == Buildings[i]) // ������ ������Ʈ�� �ǹ� ������Ʈ���� Ȯ��
            {
                return true;
            }
        }
        return false; // �ǹ� ������Ʈ�� �ƴ� ��� false ��ȯ
    }
}
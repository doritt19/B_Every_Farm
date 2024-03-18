using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmEditor : MonoBehaviour
{
    public Transform selectedBuilding; // ���õ� �ǹ�
    public Vector3 initialPosition; // �ʱ� ��ġ
    private bool isDragging = false; // �ǹ��� �巡�� ������ ���θ� ��Ÿ���� ����
    private Vector3 offset; // ���콺 Ŭ�� ��ġ�� �ǹ��� �߽��� ���� ����
    public Tilemap tilemap; // Ÿ�ϸ��� ĭ�� ���� �ǹ��� �̵��ϱ� ���� Ÿ�ϸ� ����

    public void EditStart()
    {
        gameObject.SetActive(!gameObject.activeSelf); // Ȱ��ȭ/��Ȱ��ȭ
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺�� �ǹ��� ����
            SelectBuilding();
        }

        if (isDragging && Input.GetMouseButtonUp(0)) // �ǹ� �巡�� ���̰� ���콺 ��ư�� �� ���
        {
            isDragging = false; // ���� �ٸ� �ǹ��� ��ģ�ٸ� �ʱ� ��ġ�� �ǵ���

            if (CheckOverlap())
            {
                selectedBuilding.position = initialPosition;
            }
        }

        if (isDragging)
        {
            MoveBuilding(); // �ǹ��� �巡�� �� ����Ͽ� ��ġ �̵�
        }
    }

    void SelectBuilding()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            selectedBuilding = hit.transform;// �ǹ��� ����
            initialPosition = selectedBuilding.position; // �ʱ� ��ġ ����
            isDragging = true; // �ǹ��� �巡�� ������ ����
        }
    }

    void MoveBuilding()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(mousePos); // ���콺 ��ġ�� Ÿ�ϸ� �׸��� �� ��ġ�� ��ȯ
        
        Vector3 targetPosition = tilemap.GetCellCenterWorld(cellPosition); // Ÿ�ϸ��� �׸��忡 �°� �ǹ��� �̵�
        selectedBuilding.position = new Vector3(targetPosition.x, targetPosition.y, selectedBuilding.position.z);
    }

    bool CheckOverlap()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(selectedBuilding.position, selectedBuilding.localScale, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.transform != selectedBuilding)
            {
                return true;
            }
        }
        return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmEditor : MonoBehaviour
{
    public Transform selectedBuilding; // 선택된 건물
    public Vector3 initialPosition; // 초기 위치
    private bool isDragging = false; // 건물을 드래그 중인지 여부를 나타내는 변수
    private Vector3 offset; // 마우스 클릭 위치와 건물의 중심점 간의 차이
    public Tilemap tilemap; // 타일맵의 칸에 맞춰 건물을 이동하기 위한 타일맵 변수

    public void EditStart()
    {
        gameObject.SetActive(!gameObject.activeSelf); // 활성화/비활성화
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스로 건물을 선택
            SelectBuilding();
        }

        if (isDragging && Input.GetMouseButtonUp(0)) // 건물 드래그 중이고 마우스 버튼을 뗀 경우
        {
            isDragging = false; // 만약 다른 건물과 겹친다면 초기 위치로 되돌림

            if (CheckOverlap())
            {
                selectedBuilding.position = initialPosition;
            }
        }

        if (isDragging)
        {
            MoveBuilding(); // 건물을 드래그 앤 드랍하여 위치 이동
        }
    }

    void SelectBuilding()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            selectedBuilding = hit.transform;// 건물을 선택
            initialPosition = selectedBuilding.position; // 초기 위치 저장
            isDragging = true; // 건물을 드래그 중으로 설정
        }
    }

    void MoveBuilding()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(mousePos); // 마우스 위치를 타일맵 그리드 셀 위치로 변환
        
        Vector3 targetPosition = tilemap.GetCellCenterWorld(cellPosition); // 타일맵의 그리드에 맞게 건물을 이동
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
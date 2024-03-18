using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmEditorXX : MonoBehaviour
{
    public Vector3 OriginalPosition; // 오브젝트의 초기 위치를 저장할 변수
    public GameObject[] Buildings; // 관리할 건물들을 할당하는 변수
    public Tilemap tilemap; // 타일맵의 칸에 맞춰 건물을 이동하기 위한 타일맵 변수
    public GameObject selectedObject; // 선택된 오브젝트를 저장하는 변수

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
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 클릭했을 때
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos); // 마우스 클릭 위치를 타일맵의 셀 좌표로 변환

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero); // 마우스 위치에서 레이캐스트를 발사하여 충돌 정보 확인

            if (hit.collider != null) // 레이캐스트가 어떤 오브젝트와 충돌했을 경우
            {
                GameObject clickedObject = hit.collider.gameObject; // 충돌한 오브젝트 저장
                if (IsBuilding(clickedObject)) // 충돌한 오브젝트가 건물인지 확인
                {
                    selectedObject = clickedObject; // 선택된 오브젝트를 저장
                    // 선택된 오브젝트의 초기 위치를 중복되지 않게 추가
                    OriginalPosition = selectedObject.transform.position;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0)) // 마우스 왼쪽 버튼을 뗐을 때
        {
            if (selectedObject != null) // 선택된 오브젝트가 있는 경우
            {
                RaycastHit2D overlap = Physics2D.Raycast(selectedObject.transform.position, Vector3.zero); // 선택된 오브젝트 위치에서 레이캐스트 발사

                if (overlap.collider != null && overlap.collider.gameObject != selectedObject) // 선택된 오브젝트와 다른 오브젝트와 겹치는 경우
                {
                    selectedObject.transform.position = OriginalPosition; // 선택된 오브젝트의 위치를 초기 위치로 되돌림
                }
                selectedObject = null; // 선택된 오브젝트 초기화
            }
        }

                if (selectedObject != null) // 선택된 오브젝트가 있는 경우
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // z축을 0으로 고정하여 오브젝트가 카메라 앞으로 나오도록 설정
            // 타일맵 셀 위치에 맞게 오브젝트 위치 조정
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);
            Vector3 targetPos = tilemap.GetCellCenterWorld(cellPosition);
            selectedObject.transform.position = targetPos;
        }
    }

    bool IsBuilding(GameObject obj)
    {
        for (int i = 0; i < Buildings.Length; i++)
        {
            if (obj == Buildings[i]) // 선택한 오브젝트가 건물 오브젝트인지 확인
            {
                return true;
            }
        }
        return false; // 건물 오브젝트가 아닌 경우 false 반환
    }
}
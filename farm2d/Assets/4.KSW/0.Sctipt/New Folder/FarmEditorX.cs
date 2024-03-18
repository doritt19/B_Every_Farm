using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmEditorX : MonoBehaviour
{
    public bool isDragging = false; // 오브젝트를 드래그 중인지 여부를 나타내는 변수
    public bool isClicked = false; // 오브젝트가 클릭되었는지 여부를 나타내는 변수
    public Vector3 offset; // 클릭 지점과 오브젝트의 거리 차이를 저장하는 변수
    public Tilemap tilemap;
    public Tilemap[] tilemaps;
    public List<Vector3> initialPositions = new List<Vector3>(); // 오브젝트의 초기 위치를 저장할 리스트
    public PolygonCollider2D PolygonCollider2D;
    private Rigidbody2D FarmEditRb;

    void Start()
    {
        // 초기 위치 저장
        initialPositions.Add(transform.position);
        PolygonCollider2D = GetComponent<PolygonCollider2D>();
        FarmEditRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // 레이가 충돌한 객체가 있고, 그 객체가 이 스크립트가 붙은 오브젝트라면
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // 클릭된 객체로 설정
                isClicked = true;

                // 드래그 시작
                isDragging = true;
                FarmEditRb.isKinematic = true;


                // 클릭한 위치와 오브젝트의 거리 차이를 계산하여 저장
                offset = transform.position - (Vector3)hit.point;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // 클릭 상태 초기화
            isClicked = false;

            // 드래그 종료
            isDragging = false;
            FarmEditRb.isKinematic = false;
        }

        // 드래그 중이고 클릭된 상태일 때
        if (isDragging && isClicked)
        {
            // 현재 마우스 위치를 기준으로 오브젝트를 이동
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            // 타일맵 그리드에 맞추기 위해 소수점을 버림
            foreach (Tilemap tilemap in tilemaps)
            {
                Vector3Int cellPosition = tilemap.WorldToCell(curPosition);
                if (tilemap.GetTile(cellPosition) == null)
                {
                    curPosition = tilemap.GetCellCenterWorld(cellPosition);

                    // z축은 고정된 값을 유지
                    curPosition.z = transform.position.z;

                    transform.position = curPosition;
                    break;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            ResetToInitialPosition();
        }
    }
    void ResetToInitialPosition()//저장된 초기위치로 되돌리는 함수
    {
        if (initialPositions.Count > 0)
        {
            transform.position = initialPositions[0];
            transform.rotation = Quaternion.identity;
        }
    }
}
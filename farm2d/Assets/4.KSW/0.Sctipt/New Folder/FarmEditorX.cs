using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmEditorX : MonoBehaviour
{
    public bool isDragging = false; // ������Ʈ�� �巡�� ������ ���θ� ��Ÿ���� ����
    public bool isClicked = false; // ������Ʈ�� Ŭ���Ǿ����� ���θ� ��Ÿ���� ����
    public Vector3 offset; // Ŭ�� ������ ������Ʈ�� �Ÿ� ���̸� �����ϴ� ����
    public Tilemap tilemap;
    public Tilemap[] tilemaps;
    public List<Vector3> initialPositions = new List<Vector3>(); // ������Ʈ�� �ʱ� ��ġ�� ������ ����Ʈ
    public PolygonCollider2D PolygonCollider2D;
    private Rigidbody2D FarmEditRb;

    void Start()
    {
        // �ʱ� ��ġ ����
        initialPositions.Add(transform.position);
        PolygonCollider2D = GetComponent<PolygonCollider2D>();
        FarmEditRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // ���̰� �浹�� ��ü�� �ְ�, �� ��ü�� �� ��ũ��Ʈ�� ���� ������Ʈ���
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Ŭ���� ��ü�� ����
                isClicked = true;

                // �巡�� ����
                isDragging = true;
                FarmEditRb.isKinematic = true;


                // Ŭ���� ��ġ�� ������Ʈ�� �Ÿ� ���̸� ����Ͽ� ����
                offset = transform.position - (Vector3)hit.point;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Ŭ�� ���� �ʱ�ȭ
            isClicked = false;

            // �巡�� ����
            isDragging = false;
            FarmEditRb.isKinematic = false;
        }

        // �巡�� ���̰� Ŭ���� ������ ��
        if (isDragging && isClicked)
        {
            // ���� ���콺 ��ġ�� �������� ������Ʈ�� �̵�
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            // Ÿ�ϸ� �׸��忡 ���߱� ���� �Ҽ����� ����
            foreach (Tilemap tilemap in tilemaps)
            {
                Vector3Int cellPosition = tilemap.WorldToCell(curPosition);
                if (tilemap.GetTile(cellPosition) == null)
                {
                    curPosition = tilemap.GetCellCenterWorld(cellPosition);

                    // z���� ������ ���� ����
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
    void ResetToInitialPosition()//����� �ʱ���ġ�� �ǵ����� �Լ�
    {
        if (initialPositions.Count > 0)
        {
            transform.position = initialPositions[0];
            transform.rotation = Quaternion.identity;
        }
    }
}
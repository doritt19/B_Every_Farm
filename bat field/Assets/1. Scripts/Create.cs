using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Create : MonoBehaviour
{
    public GameObject[,] tiles = new GameObject[8, 8]; // 8x8 ũ���� Ÿ�ϸ� �迭
    public float tileSize = 1.0f; // Ÿ���� ũ��

    void Start()
    {
        // Ÿ�ϸ��� �� ��Ͽ� ���� ������ �ʱ�ȭ
        InitializeTileMap();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ���콺 Ŭ���� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ���̼Ҹ�Ʈ�� ��ǥ�� ��ȯ
            Vector2 isoPosition = CartesianToIso(clickPosition);

            // �ش� Ÿ���� �ε��� ���
            int tileX = Mathf.FloorToInt(isoPosition.x / tileSize);
            int tileY = Mathf.FloorToInt(isoPosition.y / tileSize);

            // �ش� Ÿ���� �ε��� ���
            Debug.Log("Clicked tile index: (" + tileX + ", " + tileY + ")");
        }
    }

    // Ÿ�ϸ� �ʱ�ȭ �Լ�
    private void InitializeTileMap()
    {
        // Ÿ�ϸ��� �� Ÿ�Ͽ� ���� GameObject ����
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                // Ÿ���� ��ġ�� ����Ͽ� �ش��ϴ� GameObject�� �������� �迭�� ����
                GameObject tileObject = GetTileObject(x, y);
                tiles[x, y] = tileObject;
            }
        }
    }

    // �־��� Ÿ���� ��ġ�� �ش��ϴ� GameObject ��������
    private GameObject GetTileObject(int x, int y)
    {
        // ���⼭�� ������ �� GameObject�� �����Ͽ� ��ȯ�ϴ� ���ø� �����ݴϴ�.
        GameObject tileObject = new GameObject("Tile_" + x + "_" + y);
        tileObject.transform.position = IsoToCartesian(new Vector2(x, y)); // Ÿ���� ��ġ ����

        // ���⼭�� ������ �� GameObject�� ��ȯ�մϴ�.
        return tileObject;
    }

    // ���̼Ҹ�Ʈ�� ��ǥ�� ���簢�� ��ǥ�� ��ȯ�ϴ� �Լ�
    private Vector3 IsoToCartesian(Vector2 isoPosition)
    {
        Vector3 cartPosition = Vector3.zero;
        cartPosition.x = (isoPosition.x - isoPosition.y) * (tileSize / 2);
        cartPosition.y = (isoPosition.x + isoPosition.y) * (tileSize / 4);
        return cartPosition;
    }

    // ���簢�� ��ǥ�� ���̼Ҹ�Ʈ�� ��ǥ�� ��ȯ�ϴ� �Լ�
    private Vector2 CartesianToIso(Vector3 cartPosition)
    {
        Vector2 isoPosition = Vector2.zero;
        isoPosition.x = (cartPosition.x / (tileSize / 2) + cartPosition.y / (tileSize / 4)) / 2;
        isoPosition.y = (cartPosition.y / (tileSize / 4) - cartPosition.x / (tileSize / 2)) / 2;
        return isoPosition;
    }
}

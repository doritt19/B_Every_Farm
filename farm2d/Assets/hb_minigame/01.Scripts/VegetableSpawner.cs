using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableSpawner : MonoBehaviour
{
    public GameObject tomatoPrefab; // ��ȯ�� tomato ������
    public float spawnInterval = 2f; // ��ȯ ���� (��)
    public float minX = -8.9f; // ��ȯ�� x�� �ּҰ�
    public float maxX = 8f; // ��ȯ�� x�� �ִ밪
    public float spawnY = 8f; // ��ȯ�� y��

    private float timer = 0f;

    void Update()
    {
        // Ÿ�̸� ������Ʈ
        timer += Time.deltaTime;

        // ���� �ð����� tomato ��ȯ
        if (timer >= spawnInterval)
        {
            SpawnTomato();
            timer = 0f;
        }
    }

    void SpawnTomato()
    {
        // ������ x ��ġ ���
        float randomX = Random.Range(minX, maxX);

        // tomato ��ȯ
        Instantiate(tomatoPrefab, new Vector3(randomX, spawnY, 0), Quaternion.identity);
    }
}

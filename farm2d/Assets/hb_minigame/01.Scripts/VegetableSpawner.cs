using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableSpawner : MonoBehaviour
{
    public GameObject tomatoPrefab; // 소환할 tomato 프리팹
    public float spawnInterval = 2f; // 소환 간격 (초)
    public float minX = -8.9f; // 소환할 x의 최소값
    public float maxX = 8f; // 소환할 x의 최대값
    public float spawnY = 8f; // 소환할 y값

    private float timer = 0f;

    void Update()
    {
        // 타이머 업데이트
        timer += Time.deltaTime;

        // 일정 시간마다 tomato 소환
        if (timer >= spawnInterval)
        {
            SpawnTomato();
            timer = 0f;
        }
    }

    void SpawnTomato()
    {
        // 랜덤한 x 위치 계산
        float randomX = Random.Range(minX, maxX);

        // tomato 소환
        Instantiate(tomatoPrefab, new Vector3(randomX, spawnY, 0), Quaternion.identity);
    }
}

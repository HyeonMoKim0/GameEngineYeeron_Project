using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private float spawnDelay; //설정된 값에 따른 스폰 간격
    [SerializeField] private float spawnDistance; // 카메라 중심으로 스폰되는 거리

    private float spawnCount;
    public GameObject[] enemyPrefabs;
    public Transform player;

    public void Start()
    {
        spawnCount = spawnDelay;
    }

    
    public void Update()
    {
        spawnCount -= Time.deltaTime;
        if (spawnCount <= 0)
        {
            spawnEnemy();
            spawnCount = spawnDelay;
        }
    }
    public void spawnEnemy()
    {

        //Vector2 spawnDir = Random.insideUnitCircle.normalized;
        Vector3 spawnDir = Random.insideUnitSphere.normalized;
        //카메라 중심에서 랜덤 방향으로 spawnDistance 만큼 떨어진 위치에 적 생성
        Vector3 spawnPos = player.transform.position + new Vector3(spawnDir.x, 0, spawnDir.z) * spawnDistance;
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[randomIndex], spawnPos, Quaternion.identity);

    }
}

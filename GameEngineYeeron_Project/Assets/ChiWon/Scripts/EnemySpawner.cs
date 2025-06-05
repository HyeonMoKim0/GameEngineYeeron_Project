using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private float spawnDelay; //������ ���� ���� ���� ����
    [SerializeField] private float spawnDistance; // ī�޶� �߽����� �����Ǵ� �Ÿ�

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
        //ī�޶� �߽ɿ��� ���� �������� spawnDistance ��ŭ ������ ��ġ�� �� ����
        Vector3 spawnPos = player.transform.position + new Vector3(spawnDir.x, 0, spawnDir.z) * spawnDistance;
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[randomIndex], spawnPos, Quaternion.identity);

    }
}

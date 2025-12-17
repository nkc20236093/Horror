using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab; // 敵のプレハブ
    [SerializeField] float minDistance = 10.0f; // プレイヤーからの最小距離
    [SerializeField] float spawnInterval = 2.0f; // 敵をスポーンする間隔

    Transform player;
    Transform[] spawnPoints;
    float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        List<Transform> list = new List<Transform>();
        foreach (Transform child in transform)
        {
            list.Add(child);
        }
        spawnPoints = list.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // タイマーを加算
        if (timer >= spawnInterval)
        {          
            timer = 0; // タイマーをリセット
            // ここで起動
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        // プレイヤーとの距離が離れているポイントを抽出
        var validPoints = new List<Transform>();
        foreach (var point in spawnPoints)
        {
            float distance = Vector3.Distance(player.position, point.position);
            if(distance >= minDistance)
            {
                validPoints.Add(point);
            }
        }
        if(validPoints.Count == 0) return; // 有効なポイントがない場合は終了
        // ランダムにスポーンポイントを選択
        int rand = Random.Range(0, validPoints.Count);
        Transform pos = validPoints[rand];
        Instantiate(enemyPrefab, pos.position, Quaternion.identity);
    }
}

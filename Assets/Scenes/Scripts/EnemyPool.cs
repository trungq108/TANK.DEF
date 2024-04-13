using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] Transform playerPosition; public Transform PlayerPosition { get { return playerPosition; } }
    [SerializeField] GameObject enemyPrefab;
    Vector3 spawnPosition = new Vector3();
    float timeSpawn = 5;
    List<GameObject> enemies = new List<GameObject>();
     

    private void Awake()
    {
        Spawm();
        InvokeRepeating("GetActive", 1, timeSpawn);
    }


    void Spawm()
    {
        for(int i = 0; i < 20; i++)
        {
            float rotateAngle = Random.Range(0, 360);
            spawnPosition = CaculatePositionAroundPlayer(10);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, rotateAngle, 0), this.transform);
            enemies.Add(enemy);
            enemy.SetActive(false);
        }

    }

    public Vector3 CaculatePositionAroundPlayer(float distandRange)
    {
        Vector3 newSpawnPos = new Vector3();
        Vector2 range = Random.insideUnitCircle.normalized * distandRange;
        Vector3 rangeToSpawn = new Vector3(range.x, 0, range.y);
        newSpawnPos = playerPosition.position + rangeToSpawn;
        return newSpawnPos;
    }

    void GetActive()
    {
        foreach(GameObject enemy in enemies)
        {
            if(!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                enemy.GetComponent<Health>().ResetHealth();
                break;
            }
        }
        if(timeSpawn > 1) { timeSpawn -= 0.1f; }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] private int numberSpawnPos;
    [SerializeField] private float[] spawnTime;
    [SerializeField] private float[] spawnTimeCounter;


    [SerializeField] private Transform[] spawnPositions;

    [SerializeField] private GameObject knifePrefab;

    private void Update()
    {
        if (GameManager.Instance.isEnded) return;
        SpawnKnife(0, 0, 20f);
        
        if (numberSpawnPos == 2)
        {
            SpawnKnife(1, 180, -20f);
        }
    }

    private void SpawnKnife(int spawnIdx, int angle, float speed)
    {
        spawnTimeCounter[spawnIdx] -= Time.deltaTime;

        if (spawnTimeCounter[spawnIdx] <= 0.7f)
        {
            spawnPositions[spawnIdx].GetChild(0).gameObject.SetActive(true);
        }
        if (spawnTimeCounter[spawnIdx] <= 0)
        {
            spawnPositions[spawnIdx].GetChild(0).gameObject.SetActive(false);
            GameObject arrow = Instantiate(knifePrefab, spawnPositions[spawnIdx].position, Quaternion.identity);
            arrow.GetComponent<Knife>().InitializeArrow(angle, speed);
            spawnTimeCounter[spawnIdx] = spawnTime[spawnIdx];

        }
    }

}

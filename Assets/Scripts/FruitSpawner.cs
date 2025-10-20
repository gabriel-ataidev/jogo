using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    List<GameObject> _spawnPos;
    [SerializeField] List<GameObject> fruitPrefabs;
    [SerializeField] List<GameObject> junkPrefabs;

    private void Awake()
    {
        _spawnPos = new List<GameObject>();
        foreach (Transform child in transform)
        {
            _spawnPos.Add(child.gameObject);
        }
    }

    private void Start()
    {
        InvokeRepeating("SpawnFruit", 0, 1);
    }

    void SpawnFruit()
    {
        int randomFruits = Random.Range(0, fruitPrefabs.Count);
        int randomJunk = Random.Range(0, junkPrefabs.Count);
        int randomPos = Random.Range(0, _spawnPos.Count);

        int random = Random.Range(0, 4);

        if (random == 3)
        {
            Instantiate(junkPrefabs[randomJunk], _spawnPos[randomPos].transform.position, Quaternion.identity);
        }
        else Instantiate(fruitPrefabs[randomFruits], _spawnPos[randomPos].transform.position, Quaternion.identity);
    }
}

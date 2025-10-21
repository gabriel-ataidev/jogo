using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    List<GameObject> _spawnPos;
    [SerializeField] List<GameObject> fruitPrefabs;

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
        int randomPos = Random.Range(0, _spawnPos.Count);

        Instantiate(fruitPrefabs[randomFruits], _spawnPos[randomPos].transform.position, Quaternion.identity);
    }
}

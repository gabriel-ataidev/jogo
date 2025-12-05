using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LanguageSpawner : MonoBehaviour
{
    List<GameObject> _spawnPos;
    [SerializeField] List<GameObject> languagePrefabs;

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
        InvokeRepeating("SpawnLanguage", 0, 1);
    }

    void SpawnLanguage()
    {
        int randomFruits = Random.Range(0, languagePrefabs.Count);
        int randomPos = Random.Range(0, _spawnPos.Count);

        Instantiate(languagePrefabs[randomFruits], _spawnPos[randomPos].transform.position, Quaternion.identity);
    }
}

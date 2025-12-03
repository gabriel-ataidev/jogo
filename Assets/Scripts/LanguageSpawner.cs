using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LanguageSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> languagePrefabs;
    [SerializeField] float spawnInterval = 1f;
    
    [Header("Configuração de Spawn Dinâmico")]
    [SerializeField] bool useDynamicSpawnPositions = true;
    [SerializeField] int numberOfSpawnPoints = 5;
    [SerializeField] float spawnPadding = 0.5f; // Margem das bordas da tela
    
    private List<Vector3> _spawnPositions = new List<Vector3>();
    private Camera _mainCamera;
    private float _spawnY;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _spawnY = transform.position.y;
    }

    private void Start()
    {
        if (useDynamicSpawnPositions)
        {
            CalculateSpawnPositions();
        }
        else
        {
            // Usar posições dos filhos (modo manual)
            foreach (Transform child in transform)
            {
                _spawnPositions.Add(child.position);
            }
        }
        
        InvokeRepeating(nameof(SpawnLanguage), 0, spawnInterval);
    }

    void CalculateSpawnPositions()
    {
        _spawnPositions.Clear();
        
        float screenWidth = GetScreenWidthInWorldUnits();
        float minX = -screenWidth / 2 + spawnPadding;
        float maxX = screenWidth / 2 - spawnPadding;
        
        // Distribuir pontos uniformemente na largura da tela
        for (int i = 0; i < numberOfSpawnPoints; i++)
        {
            float t = numberOfSpawnPoints > 1 ? (float)i / (numberOfSpawnPoints - 1) : 0.5f;
            float xPos = Mathf.Lerp(minX, maxX, t);
            _spawnPositions.Add(new Vector3(xPos, _spawnY, 0));
        }
        
        Debug.Log($"LanguageSpawner: {numberOfSpawnPoints} spawn points criados de X={minX:F2} até X={maxX:F2}");
    }

    float GetScreenWidthInWorldUnits()
    {
        if (_mainCamera == null) return 10f;
        
        if (_mainCamera.orthographic)
        {
            float screenAspect = (float)Screen.width / Screen.height;
            return _mainCamera.orthographicSize * 2 * screenAspect;
        }
        else
        {
            // Câmera em perspectiva
            float distance = Mathf.Abs(_mainCamera.transform.position.z - _spawnY);
            float screenHeight = 2.0f * distance * Mathf.Tan(_mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            return screenHeight * _mainCamera.aspect;
        }
    }

    void SpawnLanguage()
    {
        if (_spawnPositions.Count == 0 || languagePrefabs.Count == 0) return;
        
        int randomLanguage = Random.Range(0, languagePrefabs.Count);
        int randomPos = Random.Range(0, _spawnPositions.Count);

        Instantiate(languagePrefabs[randomLanguage], _spawnPositions[randomPos], Quaternion.identity);
    }
}


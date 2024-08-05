using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyType[] _enemies;
    [SerializeField] private float _initialSpawnInterval = 2f;
    [SerializeField] private float _minimumSpawnInterval = 0.5f;
    [SerializeField] private float _spawnDistance = 10f;
    [SerializeField] private Camera _camera;

    private float _currentSpawnInterval;
    private float _timeSinceLastAdjustment;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        _currentSpawnInterval = _initialSpawnInterval;
        _timeSinceLastAdjustment = 0f;

        StartCoroutine(SpawnEnemiesRoutine());
    }

    private void Update()
    {
        _timeSinceLastAdjustment += Time.deltaTime;

        if (_timeSinceLastAdjustment >= 10f)
        {
            _timeSinceLastAdjustment = 0f;
            _currentSpawnInterval = Mathf.Max(_minimumSpawnInterval, _currentSpawnInterval - 0.1f);
        }
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_currentSpawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = GetRandomEnemy();
        if (enemyToSpawn != null)
        {
            Vector3 spawnPosition = GetSpawnPositionOutsideCamera();
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private GameObject GetRandomEnemy()
    {

        float randomValue = Random.value;

        foreach (EnemyType enemy in _enemies)
        {
            if (randomValue <= enemy.SpawnChance)
            {
                return enemy.EnemyPrefab;
            }
        }

        return null;
    }

    private Vector3 GetSpawnPositionOutsideCamera()
    {
        float camHeight = 2f * _camera.orthographicSize;
        float camWidth = camHeight * _camera.aspect;

        int side = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (side)
        {
            case 0:
                spawnPosition = new Vector3(_camera.transform.position.x - camWidth / 2 - _spawnDistance, Random.Range(_camera.transform.position.y - camHeight / 2, _camera.transform.position.y + camHeight / 2), 0);
                break;
            case 1:
                spawnPosition = new Vector3(_camera.transform.position.x + camWidth / 2 + _spawnDistance, Random.Range(_camera.transform.position.y - camHeight / 2, _camera.transform.position.y + camHeight / 2), 0);
                break;
            case 2:
                spawnPosition = new Vector3(Random.Range(_camera.transform.position.x - camWidth / 2, _camera.transform.position.x + camWidth / 2), _camera.transform.position.y + camHeight / 2 + _spawnDistance, 0);
                break;
            case 3:
                spawnPosition = new Vector3(Random.Range(_camera.transform.position.x - camWidth / 2, _camera.transform.position.x + camWidth / 2), _camera.transform.position.y - camHeight / 2 - _spawnDistance, 0);
                break;
        }

        return spawnPosition;
    }
}

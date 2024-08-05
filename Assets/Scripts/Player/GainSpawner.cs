using System.Collections;
using UnityEngine;

public class GainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _gainPrefabs;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private float _destroyTime;
    [SerializeField] private float _side = 2f;

    private void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }

        StartCoroutine(SpawnGainRoutine());
    }

    private IEnumerator SpawnGainRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            SpawnGainBonus();
        }
    }

    private void SpawnGainBonus()
    {
        GameObject newGain = GetRandomGainExcludingCurrent();

        if (newGain != null)
        {
            Vector3 spawnPosition = GetRandomPositionInCameraBounds();

            GameObject gain = Instantiate(newGain, spawnPosition, Quaternion.identity);
            Destroy(gain, _destroyTime);
        }
    }

    private GameObject GetRandomGainExcludingCurrent()
    {
        if (_gainPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, _gainPrefabs.Length);
            return _gainPrefabs[randomIndex];
        }

        return null;
    }

    private Vector3 GetRandomPositionInCameraBounds()
    {
        float camHeight = 2f * _mainCamera.orthographicSize;
        float camWidth = camHeight * _mainCamera.aspect;

        float randomX = Random.Range(_mainCamera.transform.position.x - camWidth / 2 + _side, _mainCamera.transform.position.x + camWidth / 2 - _side);
        float randomY = Random.Range(_mainCamera.transform.position.y - camHeight / 2 + _side, _mainCamera.transform.position.y + camHeight / 2 - _side);

        return new Vector3(randomX, randomY, 0);
    }
}

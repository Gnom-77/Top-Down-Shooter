using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _weaponPrefabs;
    [SerializeField] private TakeWeapon _playerTakeWeapon;
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

        StartCoroutine(SpawnWeaponRoutine());
    }

    private IEnumerator SpawnWeaponRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            SpawnWeaponBonus();
        }
    }

    private void SpawnWeaponBonus()
    {
        GameObject newWeapon = GetRandomWeaponExcludingCurrent();

        if (newWeapon != null)
        {
            Vector3 spawnPosition = GetRandomPositionInCameraBounds();

            GameObject weapon = Instantiate(newWeapon, spawnPosition, Quaternion.identity);
            Destroy(weapon, _destroyTime);
        }
    }

    private GameObject GetRandomWeaponExcludingCurrent()
    {
        List<GameObject> availableWeapons = new();

        foreach (GameObject weapon in _weaponPrefabs)
        {
            if (weapon.name != _playerTakeWeapon.GetCurrentWeaponName())
            {
                availableWeapons.Add(weapon);
            }
        }

        if (availableWeapons.Count > 0)
        {
            int randomIndex = Random.Range(0, availableWeapons.Count);
            return availableWeapons[randomIndex];
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

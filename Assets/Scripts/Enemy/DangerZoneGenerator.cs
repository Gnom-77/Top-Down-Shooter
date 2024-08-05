using System.Collections.Generic;
using UnityEngine;

public class DangerZoneGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _slowZonePrefab;
    [SerializeField] private GameObject _deathZonePrefab;
    [SerializeField] private int _slowZoneCount = 3;
    [SerializeField] private int _deathZoneCount = 2;
    [SerializeField] private float _slowZoneRadius = 3f;
    [SerializeField] private float _deathZoneRadius = 1f;
    [SerializeField] private float _minimumDistanceBetweenZones = 3f;
    [SerializeField] private float _minimumDistanceFromPlayer = 3f;
    [SerializeField] private Vector2 _minBorder = new(-20, -15);
    [SerializeField] private Vector2 _maxBorder = new(20, 15);

    private readonly List<Vector3> _zonePositions = new ();
    private Vector3 _playerPosition = Vector3.zero;
    private readonly List<float> _zoneRadii = new ();

    private void Start()
    {
        GenerateZones(_slowZonePrefab, _slowZoneCount, _slowZoneRadius);
        GenerateZones(_deathZonePrefab, _deathZoneCount, _deathZoneRadius);
    }

    private void GenerateZones(GameObject zonePrefab, int zoneCount, float zoneRadius)
    {
        for (int i = 0; i < zoneCount; i++)
        {
            Vector3 newZonePosition;

            
            int attempts = 0;
            do
            {
                attempts++;
                newZonePosition = new Vector3(
                                  Random.Range(_minBorder.x + zoneRadius + _minimumDistanceBetweenZones, _maxBorder.x - zoneRadius - _minimumDistanceBetweenZones),
                                  Random.Range(_minBorder.y + zoneRadius + _minimumDistanceBetweenZones, _maxBorder.y - zoneRadius - _minimumDistanceBetweenZones),
                                  0f
                              );
            } while (!IsPositionValid(newZonePosition, zoneRadius) && attempts < 100);

            if (attempts < 100)
            {
                GameObject newZone = Instantiate(zonePrefab, newZonePosition, Quaternion.identity);
                newZone.transform.localScale = new Vector3(zoneRadius * 2, zoneRadius * 2, 1f);

                _zonePositions.Add(newZonePosition);
                _zoneRadii.Add(zoneRadius);
            }
        }
    }

    private bool IsPositionValid(Vector3 position, float radius)
    {
        if (Vector3.Distance(position, _playerPosition) < radius + _minimumDistanceFromPlayer)
        {
            return false;
        }

        for (int i = 0; i < _zonePositions.Count; i++)
        {
            float existingZoneRadius = _zoneRadii[i];
            if (Vector3.Distance(position, _zonePositions[i]) < radius + existingZoneRadius + _minimumDistanceBetweenZones)
            {
                return false;
            }
        }
        return true;
    }
}

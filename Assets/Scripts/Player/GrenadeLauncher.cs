using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] private GameObject _grenadePrefab;
    [SerializeField] private PlayerMovement _player;

    private float _nextTimeToFire = 0f;
    private bool _canShoot = false;


    private void Update()
    {
        if (Input.GetButton("Fire1") && (Time.time >= _nextTimeToFire))
        {
            _canShoot = true;

        }
        if (_canShoot )
        {
            Shoot();
            _nextTimeToFire = Time.time + 1f / _fireRate;
            _canShoot = false;
        }
    }

    private void Shoot()
    {
        Vector3 _targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition.z = 0; 


        GameObject grenade = _bulletPool.GetGrenade();
        grenade.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);

        Granade grenadeComponent = grenade.GetComponent<Granade>();
        grenadeComponent.Launch(_targetPosition);
    }


}

using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 2f;

    private float _nextTimeToFire = 0f;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire)
        {
            Shoot();
            _nextTimeToFire = Time.time + 1f / _fireRate;  
        }
    }

    private void Shoot()
    {
        GameObject bullet = _bulletPool.GetBullet();
        bullet.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);
    }

}

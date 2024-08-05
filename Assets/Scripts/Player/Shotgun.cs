using UnityEngine;

public class Shotgun : MonoBehaviour
{
    
    [SerializeField] private Transform firePoint;
    [SerializeField] private int pelletCount = 5;
    [SerializeField] private float _spreadAngle = 10f; 
    [SerializeField] private float _fireRate = 2f;
    [SerializeField] private BulletPool _bulletPool;


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
        float baseAngle = firePoint.rotation.eulerAngles.z;

        float totalSpread = _spreadAngle * (pelletCount - 1);
        float startAngle = baseAngle - totalSpread / 2;

        for (int i = 0; i < pelletCount; i++)
        {
            float angle = startAngle + i * _spreadAngle;

            GameObject bullet = _bulletPool.GetBullet();
            bullet.transform.SetPositionAndRotation(firePoint.position, Quaternion.Euler(0, 0, angle));

        }
    }
}

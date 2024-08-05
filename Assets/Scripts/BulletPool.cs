using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private bool _isGrenade;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _poolSize = 20;

    private Queue<GameObject> _bulletPool;

    private void Awake()
    {
        _bulletPool = new Queue<GameObject>();
        if (_isGrenade)
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject bullet = Instantiate(_bulletPrefab);
                bullet.SetActive(false);
                bullet.GetComponent<Granade>().SetPool(this);
                _bulletPool.Enqueue(bullet);
            }
        }
        else
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject bullet = Instantiate(_bulletPrefab);
                bullet.SetActive(false);
                bullet.GetComponent<Bullet>().SetPool(this);
                _bulletPool.Enqueue(bullet);
            }
        }

    }

    public GameObject GetBullet()
    {
        if (_bulletPool.Count > 0)
        {
            GameObject bullet = _bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.GetComponent<Bullet>().SetPool(this);
            return bullet;
        }
    }

    public GameObject GetGrenade()
    {
        if (_bulletPool.Count > 0)
        {
            GameObject bullet = _bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.GetComponent<Granade>().SetPool(this);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false); 
        _bulletPool.Enqueue(bullet);
    }


}

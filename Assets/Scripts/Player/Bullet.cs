using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private bool _isLimitedInRange;
    [SerializeField] private float _defaultLifeTime = 2.0f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody2D _bulletRb2D;

    private BulletPool _bulletPool;
    private Coroutine _lifetimeCoroutine;
    private float _lifetime;

    private void OnEnable()
    {
        if (_isLimitedInRange)
        {
            _lifetime = _range / _speed;
        }
        else
        {
            _lifetime = _defaultLifeTime;
        }
        _lifetimeCoroutine = StartCoroutine(LifetimeCoroutine());
    }


    private void FixedUpdate()
    {
        _bulletRb2D.velocity = _bulletRb2D.transform.up * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }
        ReturnToPool();
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(_lifetime);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (_lifetimeCoroutine != null)
        {
            StopCoroutine(_lifetimeCoroutine);
        }

        _bulletPool.ReturnBullet(gameObject);
    }

    public void SetPool(BulletPool pool)
    {
        _bulletPool = pool;
    }

}

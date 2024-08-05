using System.Collections;
using UnityEngine;

public class Granade : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private float _explosionDuration = 1f;
    [SerializeField] private Rigidbody2D _bulletRb2D;
    [SerializeField] private GameObject _boomSprite;

    private Vector3 _distanceToTarget;
    private bool _isExploding = false;
    private BulletPool _bulletPool;

    private void OnEnable()
    {
        _boomSprite.SetActive(false);
    }

    private void Update()
    {
        if (_isExploding) return;

        MoveTowardsTarget();

        if (Vector2.Distance(transform.position, _distanceToTarget) < 0.1f)
        {
            Explode();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (_distanceToTarget - transform.position).normalized;
        _bulletRb2D.velocity = direction * _speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }



    private void Explode()
    {
        _boomSprite.SetActive(true);
        _isExploding = true;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().TakeDamage(_damage); 
            }
        }

        StartCoroutine(HandleExplosionDuration());
    }

    private IEnumerator HandleExplosionDuration()
    {
        yield return new WaitForSeconds(_explosionDuration);
        ReturnToPool();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }

    private void ReturnToPool()
    {
        _isExploding = false;
        _bulletPool.ReturnBullet(gameObject);
    }

    public void SetPool(BulletPool pool)
    {
        _bulletPool = pool;
    }

    public void Launch(Vector3 distanceToTarget)
    {
        _distanceToTarget = distanceToTarget;
    }
}

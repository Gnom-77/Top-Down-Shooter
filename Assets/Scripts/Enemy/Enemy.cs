using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _enemyRb2D;
    [SerializeField] private float _speed;
    [SerializeField] private int _hp;
    [SerializeField] private int _cost;

    private Transform _player;
    private float _counter;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            _player = GameManager.Instance.PlayerTransform;
            _counter = 0;
        }
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            Vector2 direction = (_player.position - transform.position).normalized * _speed;
            _enemyRb2D.MovePosition(_enemyRb2D.position + direction  * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage (int damage)
    {
        _hp -= damage;
        if (_hp <= 0 && _counter < 1)
        {
            ScoreCount.Count += _cost;
            _counter += 1;
            Destroy(this.gameObject);
        }
    }
}
